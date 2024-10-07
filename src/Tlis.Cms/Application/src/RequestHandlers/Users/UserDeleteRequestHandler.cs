using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Users;

internal sealed class UserDeleteRequestHandler(
    IAuthProviderManagementService authProviderManagementService,
    ICloudeStorageService storageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UserDeleteRequest, bool>
{
    public async Task<bool> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: false);
        if (user is null) return false;

        await unitOfWork.UserRepository.DeleteByIdAsync(request.Id);
        if (user.ProfileImageId is not null)
        {
            await unitOfWork.ImageRepository.DeleteByIdAsync(user.ProfileImageId.Value);
        }

        await unitOfWork.SaveChangesAsync();

        if (string.IsNullOrEmpty(user.ExternalId) is false)
        {
            await authProviderManagementService.DeleteUser(user.ExternalId);
        }

        if (user.ProfileImage is not null)
        {
            await storageService.DeleteUserImage(user.ProfileImage.FileName);

            foreach (var crop in user.ProfileImage.Crops)
            {
                await storageService.DeleteUserImage(crop.Url);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}