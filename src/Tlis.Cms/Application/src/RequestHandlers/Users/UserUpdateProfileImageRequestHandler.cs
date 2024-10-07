using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Users;

internal sealed class UserUpdateProfileImageRequestHandler(
    IImageProcessingService imageProcessingService,
    ICloudeStorageService storageService,
    IOptions<ImageProcessingConfiguration> imageProcessingConfiguration,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UserUpdateProfileImageRequest, bool>
{
    public async Task<bool> Handle(UserUpdateProfileImageRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: true);
        if (user is null)
        {
            return false;
        }

        if (user.ProfileImage is not null)
        {
            await storageService.DeleteUserImage(user.ProfileImage.FileName);

            foreach (var crop in user.ProfileImage.Crops)
            {
                await storageService.DeleteUserImage(crop.Url);
            }
        }

        var profileImage = await imageProcessingService.ProcessImageAsync(
            request.ProfileImage,
            ImageType.User,
            imageProcessingConfiguration.Value.User);

        await unitOfWork.ImageRepository.InsertAsync(profileImage);

        await unitOfWork.SaveChangesAsync();

        user.ProfileImageId = profileImage.Id;

        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}