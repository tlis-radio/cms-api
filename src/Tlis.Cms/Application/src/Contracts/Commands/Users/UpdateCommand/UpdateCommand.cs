using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.Application.Contracts.Commands.Users.UpdateCommand;

public sealed class UpdateCommand : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public required bool CmsAdminAccess { get; set; }

    [SwaggerSchema(Description = "User's first name")]
    public required string Firstname { get; set; }

    [SwaggerSchema(Description = "User's last name")]
    public required string Lastname { get; set; }

    [SwaggerSchema(Description = "User's nickname or alias")]
    public required string Nickname { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public required string? Email { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    public required string Abouth { get; set; }

    public required bool PreferNicknameOverName { get; set; }

    public required List<UpdateCommandRoleHistory> RoleHistory { get; set; } = [];

    public required List<UpdateCommandMembershipHistory> MembershipHistory { get; set; } = [];
}