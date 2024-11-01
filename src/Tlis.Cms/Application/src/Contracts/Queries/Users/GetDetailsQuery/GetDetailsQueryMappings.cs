using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.Contracts.Queries.Users.GetDetailsQuery;

public static class GetDetailsQueryMappings
{
    public static GetDetailsQueryResponse? MapToResponse(this User? entity, ICloudeStorageService cloudeStorageService)
    {
        if (entity == null)
        {
            return null;
        }

        var profileImageUrl = cloudeStorageService.GetUserImageUrl(entity.ProfileImage?.FileName);

        var response = new GetDetailsQueryResponse
        {
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Nickname = entity.Nickname,
            PreferNicknameOverName = entity.PreferNicknameOverName,
            CmsAdminAccess = entity.CmsAdminAccess,
            Abouth = entity.Abouth,
            ExternalId = entity.ExternalId,
            MembershipHistory = entity.MembershipHistory.Select(MapToGetDetailsQueryResponseMembershipHistory).ToList(),
            RoleHistory = entity.RoleHistory.Select(MapToGetDetailsQueryResponseRoleHistory).ToList(),
            ProfileImage = entity.ProfileImage is null || profileImageUrl is null
                ? null
                : new GetDetailsQueryResponseImage
                {
                    Id = entity.ProfileImage.Id,
                    Url = profileImageUrl
                }
        };

        return response;
    }

    private static GetDetailsQueryResponseMembershipHistory MapToGetDetailsQueryResponseMembershipHistory(UserMembershipHistory entity)
    {
        return new GetDetailsQueryResponseMembershipHistory
        {
            Id = entity.Id,
            ChangeDate = entity.ChangeDate,
            Description = entity.Description,
            Membership = new GetDetailsQueryResponseMembershipHistoryMembership
            {
                Id = entity.Membership.Id,
                Status = entity.Membership.Status
            }
        };
    }

    private static GetDetailsQueryResponseRoleHistory MapToGetDetailsQueryResponseRoleHistory(UserRoleHistory entity)
    {
        return new GetDetailsQueryResponseRoleHistory
        {
            Id = entity.Id,
            Role = MapToGetDetailsQueryResponseRoleHistoryRole(entity.Role),
            FunctionStartDate = entity.FunctionStartDate,
            FunctionEndDate = entity.FunctionEndDate,
            Description = entity.Description
        };
    }

    private static GetDetailsQueryResponseRoleHistoryRole MapToGetDetailsQueryResponseRoleHistoryRole(Role role)
    {
        return new GetDetailsQueryResponseRoleHistoryRole
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}