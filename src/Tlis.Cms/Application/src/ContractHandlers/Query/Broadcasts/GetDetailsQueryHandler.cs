using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetDetailsQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Broadcasts;

internal sealed class GetDetailsQueryHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService) : IRequestHandler<GetDetailsQuery, GetDetailsQueryResponse?>
{
    public async Task<GetDetailsQueryResponse?> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
    {
        var broadcast = await unitOfWork.BroadcastRepository.GetByIdAsync(request.Id, asTracking: false);

        return broadcast.MapToResponse(cloudeStorageService.GetBroadcastImageUrl(broadcast?.Image?.FileName));
    }
}