using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Users.DeleteCommand;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Users;

internal sealed class DeleteCommandHandler(
    IAuthProviderManagementService authProviderManagementService,
    ICloudeStorageService storageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCommand, bool>
{
    public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
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
            await authProviderManagementService.DeleteUserAsync(user.ExternalId);
        }

        if (user.ProfileImage is not null)
        {
            await storageService.DeleteUserImageAsync(user.ProfileImage.FileName);

            foreach (var crop in user.ProfileImage.Crops)
            {
                await storageService.DeleteUserImageAsync(crop.FileName);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}