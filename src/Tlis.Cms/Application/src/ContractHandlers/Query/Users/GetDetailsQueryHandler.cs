using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Users.GetDetailsQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Users;

internal sealed class GetDetailsQueryHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService)
    : IRequestHandler<GetDetailsQuery, GetDetailsQueryResponse?>
{
    public async Task<GetDetailsQueryResponse?> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: false);

        return user.MapToResponse(cloudeStorageService);
    }
}