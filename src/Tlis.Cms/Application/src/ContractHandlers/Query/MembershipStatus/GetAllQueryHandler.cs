using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.MembershipStatus.GetAllQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.MembershipStatus;

internal sealed class GetAllQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllQuery, GetAllQueryResponse>
{
    public async Task<GetAllQueryResponse> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var memberships = await unitOfWork.MembershipRepository.GetAll();

        return memberships.MapToResponse();
    }
}