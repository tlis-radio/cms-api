using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Exceptions;
using Tlis.Cms.Application.Mappings;
using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Services.Interfaces;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Users;

internal sealed class UserCreateRequestHandler(
    IAuthProviderManagementService authProviderManagementService,
    IUserRoleService roleService,
    IUnitOfWork unitOfWork) : IRequestHandler<UserCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var newUser = UserMappings.MapToUser(request);

        foreach (var membershipHistory in request.MembershipHistory)
        {
            newUser.MembershipHistory.Add(new UserMembershipHistory
            {
                MembershipId = membershipHistory.MembershipId,
                ChangeDate = membershipHistory.ChangeDate,
                Description = membershipHistory.Description
            });
        }

        foreach (var history in request.RoleHistory)
        {
            var role = await roleService.GetByIdAsync(history.RoleId)
                ?? throw new UserRoleNotFoundException(history.RoleId);

            newUser.RoleHistory.Add(
                new UserRoleHistory
                {
                    RoleId = role.Id,
                    FunctionStartDate = history.FunctionStartDate,
                    FunctionEndDate = history.FunctionEndDate,
                    Description = history.Description
                }
            );
        }

        var roles = await unitOfWork.RoleRepository.GetByIdsAsync(
            newUser.RoleHistory.Select(x => x.RoleId).ToList(),
            asTracking: false);

        if (!string.IsNullOrEmpty(request.Email) && request.CmsAdminAccess)
        {
            newUser.ExternalId = await authProviderManagementService.CreateUser(
                request.Email,
                roles.Select(x => x.ExternalId).ToArray());
        }

        await unitOfWork.UserRepository.InsertAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return new BaseCreateResponse
        {
            Id = newUser.Id
        };
    }
}