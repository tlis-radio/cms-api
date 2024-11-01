using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Shows.GetDetailsQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Shows;

internal sealed class GetDetailsQueryHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService)
    : IRequestHandler<GetDetailsQuery, GetDetailsQueryResponse?>
{
    public async Task<GetDetailsQueryResponse?> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
    {
        var show = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);

        return show.MapToResponse(cloudeStorageService);
    }
}