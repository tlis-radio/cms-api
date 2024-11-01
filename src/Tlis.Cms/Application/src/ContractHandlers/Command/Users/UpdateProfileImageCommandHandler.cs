using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Contracts.Commands.Users.UpdateProfileImageCommand;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Users;

internal sealed class UpdateProfileImageCommandHandler(
    IImageProcessingService imageProcessingService,
    ICloudeStorageService storageService,
    IOptions<ImageProcessingConfiguration> imageProcessingConfiguration,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProfileImageCommand, bool>
{
    public async Task<bool> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: true);
        if (user is null)
        {
            return false;
        }

        if (user.ProfileImage is not null)
        {
            await storageService.DeleteUserImageAsync(user.ProfileImage.FileName);

            foreach (var crop in user.ProfileImage.Crops)
            {
                await storageService.DeleteUserImageAsync(crop.FileName);
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