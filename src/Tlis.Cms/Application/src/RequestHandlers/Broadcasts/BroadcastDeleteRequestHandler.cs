using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastDeleteRequestHandler(
    IStorageService storageService,
    IUnitOfWork unitOfWork) : IRequestHandler<BroadcastDeleteRequest, bool>
{
    public async Task<bool> Handle(BroadcastDeleteRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var broadcast = await unitOfWork.BroadcastRepository.GetByIdAsync(request.Id, asTracking: false);
        if (broadcast is null) return false;

        await unitOfWork.BroadcastRepository.DeleteByIdAsync(request.Id);
        if (broadcast.ImageId is not null)
        {
            await unitOfWork.ImageRepository.DeleteByIdAsync(broadcast.ImageId.Value);
        }

        await unitOfWork.SaveChangesAsync();

        if (broadcast.Image is not null)
        {
            await storageService.DeleteImage(broadcast.Image.Url);

            foreach (var crop in broadcast.Image.Crops)
            {
                await storageService.DeleteImage(crop.Url);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}