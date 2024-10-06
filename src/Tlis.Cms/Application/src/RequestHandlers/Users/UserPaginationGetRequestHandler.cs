using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Users;

internal sealed class UserPaginationGetRequestHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UserPaginationGetRequest, PaginationResponse<UserPaginationGetResponse>>
{
    public async Task<PaginationResponse<UserPaginationGetResponse>> Handle(UserPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.PaginationAsync(
            request.Limit,
            request.Page);

        return new PaginationResponse<UserPaginationGetResponse>
        {
            Total = users.Total,
            Limit = users.Limit,
            Page = users.Page,
            TotalPages = users.TotalPages,
            Results = users.Results.Select(UserMappings.MapToUserPaginationGetResponse).ToList()
        };
    }
}