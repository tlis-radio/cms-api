using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Users;

internal sealed class UserGetRequestHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService)
    : IRequestHandler<UserDetailsGetRequest, UserGetResponse?>
{
    public async Task<UserGetResponse?> Handle(UserDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: false);

        return UserMappings.MapToUserGetResponse(user, cloudeStorageService.GetUserImageUrl(user?.ProfileImage?.FileName));
    }
}