using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Shows;

internal sealed class ShowUpdateProfileImageRequestHandler(
    IImageProcessingService imageProcessingService,
    IStorageService storageService,
    IOptions<ImageProcessingConfiguration> imageProcessingConfiguration,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ShowUpdateProfileImageRequest, bool>
{
    public async Task<bool> Handle(ShowUpdateProfileImageRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var show = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, asTracking: true);
        if (show is null)
        {
            return false;
        }

        if (show.ProfileImage is not null)
        {
            await storageService.DeleteImage(show.ProfileImage.Url);

            foreach (var crop in show.ProfileImage.Crops)
            {
                await storageService.DeleteImage(crop.Url);
            }
        }

        var profileImage = await imageProcessingService.CreateImageAsync(
            request.ProfileImage,
            imageProcessingConfiguration.Value.Show);

        await unitOfWork.ImageRepository.InsertAsync(profileImage);

        await unitOfWork.SaveChangesAsync();

        show.ProfileImageId = profileImage.Id;

        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}