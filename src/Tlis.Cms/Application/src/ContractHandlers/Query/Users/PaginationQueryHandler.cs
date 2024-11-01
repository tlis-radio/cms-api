using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Queries.Users.PaginationQuery;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Query.Users;

internal sealed class PaginationQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<PaginationQuery, PaginationQueryResponse>
{
    public async Task<PaginationQueryResponse> Handle(PaginationQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.PaginationAsync(request.Limit, request.Page);

        return users.MapToResponse();
    }
}