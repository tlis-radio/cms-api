using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Shows.DeleteCommand;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Shows;

internal sealed class DeleteCommandHandler(ICloudeStorageService storageService, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommand, bool>
{
    public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
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
            await storageService.DeleteShowImageAsync(show.ProfileImage.FileName);

            foreach (var crop in show.ProfileImage.Crops)
            {
                await storageService.DeleteShowImageAsync(crop.FileName);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}