using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Shows;

internal sealed class ShowDeleteRequestHandler(
    IStorageService storageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ShowDeleteRequest, bool>
{
    public async Task<bool> Handle(ShowDeleteRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var show = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, asTracking: false);
        if (show is null) return false;

        await unitOfWork.ShowRepository.DeleteByIdAsync(request.Id);
        if (show.ProfileImageId is not null)
        {
            await unitOfWork.ImageRepository.DeleteByIdAsync(show.ProfileImageId.Value);
        }

        await unitOfWork.SaveChangesAsync();

        if (show.ProfileImage is not null)
        {
            await storageService.DeleteImage(show.ProfileImage.Url);

            foreach (var crop in show.ProfileImage.Crops)
            {
                await storageService.DeleteImage(crop.Url);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}