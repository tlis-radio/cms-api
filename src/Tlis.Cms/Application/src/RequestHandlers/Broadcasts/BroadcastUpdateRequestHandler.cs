using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastUpdateRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<BroadcastUpdateRequest, bool>
{
    public async Task<bool> Handle(BroadcastUpdateRequest request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.BroadcastRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.Name = request.Name;
        toUpdate.Description = request.Description;
        toUpdate.StartDate = request.StartDate;
        toUpdate.EndDate = request.EndDate;
        toUpdate.ShowId = request.ShowId;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}