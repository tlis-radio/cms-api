using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Broadcasts.PaginationQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Broadcasts;

internal sealed class PaginationQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<PaginationQuery, PaginationQueryResponse>
{
    public async Task<PaginationQueryResponse> Handle(PaginationQuery request, CancellationToken cancellationToken)
    {
        var broadcasts = await unitOfWork.BroadcastRepository.PaginationAsync(request.Limit, request.Page);

        return broadcasts.MapToResponse();
    }
}