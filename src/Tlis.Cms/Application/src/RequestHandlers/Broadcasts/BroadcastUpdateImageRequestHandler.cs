using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastUpdateImageRequestHandler(
    IImageProcessingService imageProcessingService,
    ICloudeStorageService storageService,
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
            await storageService.DeleteBroadcastImageAsync(broadcast.Image.FileName);

            foreach (var crop in broadcast.Image.Crops)
            {
                await storageService.DeleteBroadcastImageAsync(crop.FileName);
            }
        }

        var image = await imageProcessingService.ProcessImageAsync(
            request.Image,
            ImageType.Broadcast,
            imageProcessingConfiguration.Value.Broadcast);

        await unitOfWork.ImageRepository.InsertAsync(image);

        await unitOfWork.SaveChangesAsync();

        broadcast.ImageId = image.Id;

        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}