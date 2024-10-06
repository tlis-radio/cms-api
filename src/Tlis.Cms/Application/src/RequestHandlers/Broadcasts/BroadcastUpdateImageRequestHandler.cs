using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastUpdateImageRequestHandler(
    IImageProcessingService imageProcessingService,
    IStorageService storageService,
    IOptions<ImageProcessingConfiguration> imageProcessingConfiguration,
    IUnitOfWork unitOfWork)
    : IRequestHandler<BroadcastUpdateImageRequest, bool>
{
    public async Task<bool> Handle(BroadcastUpdateImageRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var broadcast = await unitOfWork.BroadcastRepository.GetByIdAsync(request.Id, asTracking: true);
        if (broadcast is null)
        {
            return false;
        }

        if (broadcast.Image is not null)
        {
            await storageService.DeleteImage(broadcast.Image.Url);

            foreach (var crop in broadcast.Image.Crops)
            {
                await storageService.DeleteImage(crop.Url);
            }
        }

        var image = await imageProcessingService.CreateImageAsync(
            request.Image,
            imageProcessingConfiguration.Value.Broadcast);

        await unitOfWork.ImageRepository.InsertAsync(image);

        await unitOfWork.SaveChangesAsync();

        broadcast.ImageId = image.Id;

        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}