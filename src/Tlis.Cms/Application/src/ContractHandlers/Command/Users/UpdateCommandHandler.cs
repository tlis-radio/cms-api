using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Users.UpdateCommand;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.ContractHandlers.Command.Users;

internal sealed class UpdateCommandHandler(
    IAuthProviderManagementService authProviderManagementService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCommand, bool>
{
    public async Task<bool> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();

        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: true);
        if (user is null)
        {
            return false;
        }

        if (user.CmsAdminAccess is false)
        {
            user.Email = request.Email;
        }

        if (user.CmsAdminAccess && request.CmsAdminAccess is false && user.ExternalId is not null)
        {
            await authProviderManagementService.DeleteUserAsync(user.ExternalId);
            user.ExternalId = null;
        }

        if (user.CmsAdminAccess is false && request.CmsAdminAccess && request.Email is not null)
        {
            var roleIds = request.RoleHistory.Select(x => x.RoleId);
            var roles = await unitOfWork.RoleRepository.GetByIdsAsync(roleIds, asTracking: false);

            user.ExternalId = await authProviderManagementService.CreateUserAsync(
                request.Email,
                roles.Select(x => x.ExternalId).ToArray());
        }

        if (user.CmsAdminAccess && request.CmsAdminAccess && user.ExternalId is not null)
        {
            var roleIds = request.RoleHistory.Select(x => x.RoleId);
            var roles = await unitOfWork.RoleRepository.GetByIdsAsync(roleIds, asTracking: false);

            await authProviderManagementService.UpdateUserRolesAsync(user.ExternalId, roles.Select(x => x.ExternalId).ToArray());
        }

        user.Firstname = request.Firstname;
        user.Lastname = request.Lastname;
        user.Nickname = request.Nickname;
        user.Abouth = request.Abouth;
        user.CmsAdminAccess = request.CmsAdminAccess;
        user.PreferNicknameOverName = request.PreferNicknameOverName;
        ResolveRoleHistory(user, request.RoleHistory);
        ResolveMembershipHistory(user, request.MembershipHistory);

        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync(cancellationToken);

        return true;
    }

    private static void ResolveRoleHistory(User existing, List<UpdateCommandRoleHistory> updatedHistory)
    {
        var added = updatedHistory.Where(x => x.Id is null).Select(x => x.MapToUserRoleHistory());

        var existingOptionsDict = existing.RoleHistory.ToDictionary(key => key.Id, value => value);
        var existingOptionsUpdated = updatedHistory.Where(x => x.Id is not null).Select(x => existingOptionsDict[x.Id!.Value].Update(x));

        existing.RoleHistory = added.Concat(existingOptionsUpdated).ToList();
    }

    private static void ResolveMembershipHistory(User existing, List<UpdateCommandMembershipHistory> updatedHistory)
    {
        var added = updatedHistory.Where(x => x.Id is null).Select(x => x.MapToUserMembershipHistory());

        var existingOptionsDict = existing.MembershipHistory.ToDictionary(key => key.Id, value => value);
        var existingOptionsUpdated = updatedHistory.Where(x => x.Id is not null).Select(x => existingOptionsDict[x.Id!.Value].Update(x));

        existing.MembershipHistory = added.Concat(existingOptionsUpdated).ToList();
    }
}