using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

public sealed class UserGetResponse
{
    public required string Firstname { get; set; }

    public required string Lastname { get; set; } 

    public required string Nickname { get; set; } 

    public required string Abouth { get; set; } 

    public required UserGetResponseImage? ProfileImage { get; set; }

    public required bool PreferNicknameOverName { get; set; }

    public required bool CmsAdminAccess { get; set; }

    public required string? ExternalId { get; set; }

    public required string? Email { get; set; }

    public required List<UserGetResponseUserRoleHistory> RoleHistory { get; set; } = [];

    public required List<UserGetResponseUserMembershipHistory> MembershipHistory { get; set; } = [];
}