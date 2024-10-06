using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastPaginationGetRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<BroadcastPaginationGetRequest, PaginationResponse<BroadcastPaginationGetResponse>>
{
    public async Task<PaginationResponse<BroadcastPaginationGetResponse>> Handle(BroadcastPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var broadcasts = await unitOfWork.BroadcastRepository.PaginationAsync(request.Limit, request.Page);

        return new PaginationResponse<BroadcastPaginationGetResponse>
        {
            Total = broadcasts.Total,
            Limit = broadcasts.Limit,
            Page = broadcasts.Page,
            TotalPages = broadcasts.TotalPages,
            Results = broadcasts.Results.Select(BroadcastMappings.ToPaginationDto).ToList()
        };
    }
}