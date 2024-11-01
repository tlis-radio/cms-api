using System.Collections.Generic;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Application.Contracts.Commands.Base;

namespace Tlis.Cms.Application.Contracts.Commands.User.CreateCommand;

public sealed class CreateCommand : IRequest<BaseCreateResponse>
{
    [SwaggerSchema(Description = "User's first name")]
    public required string Firstname { get; set; }

    [SwaggerSchema(Description = "User's last name")]
    public required string Lastname { get; set; }

    [SwaggerSchema(Description = "User's nickname or alias")]
    public required string Nickname { get; set; }

    [SwaggerSchema(Description = "If user prefers to show his nickname or name on main page")]
    public required bool PreferNicknameOverName { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    public required string Abouth { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public required string? Email { get; set; }

    public required bool CmsAdminAccess { get; set; }

    [SwaggerSchema(Description = "User's role history.")]
    public required List<CreateCommandRoleHistory> RoleHistory { get; set; } = [];

    [SwaggerSchema(Description = "User's membership history.")]
    public required List<CreateCommandMembershipHistory> MembershipHistory { get; set; } = []; 
}