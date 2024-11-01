using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Queries.User.GetDetailsQuery;

public sealed class GetDetailsQueryResponse
{
    public required string Firstname { get; set; }

    public required string Lastname { get; set; } 

    public required string Nickname { get; set; } 

    public required string Abouth { get; set; } 

    public required GetDetailsQueryResponseImage? ProfileImage { get; set; }

    public required bool PreferNicknameOverName { get; set; }

    public required bool CmsAdminAccess { get; set; }

    public required string? ExternalId { get; set; }

    public required string? Email { get; set; }

    public required List<GetDetailsQueryResponseRoleHistory> RoleHistory { get; set; } = [];

    public required List<GetDetailsQueryResponseMembershipHistory> MembershipHistory { get; set; } = [];
}