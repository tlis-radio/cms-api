using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests;
using System.Linq;
using Tlis.Cms.Application.Contracts.Api.Responses.RoleGetAllResponses;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Application.Mappings;

namespace Tlis.Cms.Application.RequestHandlers;

internal sealed class RoleGetAllRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RoleGetAllRequest, RoleGetAllResponse>
{
    public async Task<RoleGetAllResponse> Handle(RoleGetAllRequest request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.RoleRepository.GetAll();

        return new RoleGetAllResponse
        {
            Results = response.Select(RoleMappings.MapToRoleGetAllResponseItem).ToList()
        };
    }
}