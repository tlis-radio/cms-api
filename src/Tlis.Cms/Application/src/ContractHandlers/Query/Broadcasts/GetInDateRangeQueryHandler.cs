using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetInDateRangeQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Broadcasts;

internal sealed class GetInDateRangeQueryHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService) : IRequestHandler<GetInDateRangeQuery, GetInDateRangeQueryResponse?>
{
    public async Task<GetInDateRangeQueryResponse?> Handle(GetInDateRangeQuery request, CancellationToken cancellationToken)
    {
        var broadcasts = await unitOfWork.BroadcastRepository.GetInDateRangeAsync(request.From, request.To);

        return broadcasts.MapToResponse(cloudeStorageService);
    }
}