using System;
using System.Linq;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Mappings;

internal static class UserMappings
{
    public static UserPaginationGetResponse MapToUserPaginationGetResponse(User entity)
    {
        var latestMembership = entity.MembershipHistory.OrderByDescending(x => x.ChangeDate).FirstOrDefault();

        var response = new UserPaginationGetResponse
        {
            Id = entity.Id,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Nickname = entity.Nickname,
            Email = entity.Email,
            CmsAdminAccess = entity.CmsAdminAccess,
            Roles = entity.RoleHistory.Select(x => x.Role!.Name).ToList(),
            Status = latestMembership != null && latestMembership.Membership != null
                ? Enum.GetName(latestMembership.Membership.Status)
                : null
        };

        return response;
    }

    public static UserGetResponse? MapToUserGetResponse(User? entity, string? profileImageUrl)
    {
        if (entity == null)
        {
            return null;
        }

        var response = new UserGetResponse
        {
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Nickname = entity.Nickname,
            PreferNicknameOverName = entity.PreferNicknameOverName,
            CmsAdminAccess = entity.CmsAdminAccess,
            Abouth = entity.Abouth,
            ExternalId = entity.ExternalId,
            MembershipHistory = entity.MembershipHistory.Select(MapToUserGetResponseUserMembershipHistory).ToList(),
            RoleHistory = entity.RoleHistory.Select(MapToUserGetResponseUserRoleHistory).ToList(),
            ProfileImage = entity.ProfileImage is null || profileImageUrl is null
                ? null
                : new UserGetResponseImage
                {
                    Id = entity.ProfileImage.Id,
                    Url = profileImageUrl
                }
        };

        return response;
    }
    
    public static User MapToUser(UserCreateRequest dto)
    {
        return new User
        {
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Nickname = dto.Nickname,
            Email = dto.Email,
            CmsAdminAccess = dto.CmsAdminAccess,
            Abouth = dto.Abouth,
            PreferNicknameOverName = dto.PreferNicknameOverName
        };
    }

    private static UserGetResponseUserRoleHistory MapToUserGetResponseUserRoleHistory(UserRoleHistory entity)
    {
        return new UserGetResponseUserRoleHistory
        {
            Id = entity.Id,
            Role = MapToUserGetResponseRole(entity.Role!),
            FunctionStartDate = entity.FunctionStartDate,
            FunctionEndDate = entity.FunctionEndDate,
            Description = entity.Description
        };
    }

    private static UserGetResponseRole MapToUserGetResponseRole(Role role)
    {
        return new UserGetResponseRole
        {
            Id = role.Id,
            Name = role.Name
        };
    }
    
    private static UserGetResponseUserMembershipHistory MapToUserGetResponseUserMembershipHistory(UserMembershipHistory entity)
    {
        return new UserGetResponseUserMembershipHistory
        {
            Id = entity.Id,
            ChangeDate = entity.ChangeDate,
            Description = entity.Description,
            Membership = new UserGetResponseUserMembershipHistoryMembership
            {
                Id = entity.Membership.Id,
                Status = entity.Membership.Status
            }
        };
    }
}