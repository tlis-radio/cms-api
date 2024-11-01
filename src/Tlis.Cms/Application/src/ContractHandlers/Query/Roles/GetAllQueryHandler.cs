using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Application.Contracts.Queries.Roles.GetAllQuery;

namespace Tlis.Cms.Application.ContractHandlers.Query.Roles;

internal sealed class GetAllQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllQuery, GetAllQueryResponse>
{
    public async Task<GetAllQueryResponse> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.RoleRepository.GetAll();

        return response.MapToResponse();
    }
}