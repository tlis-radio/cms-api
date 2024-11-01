using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Shows.PaginationQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Shows;

internal sealed class PaginationQueryHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService)
    : IRequestHandler<PaginationQuery, PaginationQueryResponse>
{
    public async Task<PaginationQueryResponse> Handle(PaginationQuery request, CancellationToken cancellationToken)
    {
        var shows = await unitOfWork.ShowRepository.PaginationAsync(request.Limit, request.Page);

        return shows.MapToResponse(cloudeStorageService);
    }
}