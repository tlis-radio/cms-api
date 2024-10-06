using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Users;

public sealed class UserUpdateRequest : IRequest<bool>
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

    public required List<UserUpdateRequestRoleHistory> RoleHistory { get; set; } = [];

    public required List<UserUpdateRequestMembershipHistory> MembershipHistory { get; set; } = [];
}

public sealed class UserUpdateRequestMembershipHistory
{
    public required Guid? Id { get; set; }

    public required Guid MembershipId { get; set; }

    public required string? Description { get; set; }

    public required DateTime ChangeDate { get; set; }
}

public sealed class UserUpdateRequestRoleHistory
{
    public required Guid? Id { get; set; }

    [SwaggerSchema(Description = "The user's role or permission level within the service or platform.")]
    public required Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The date on which the user began their current role or position within TLIS.")]
    public required DateTime FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public required DateTime? FunctionEndDate { get; set; }

    [SwaggerSchema(Description = "Description why this position was given to user.")]
    public required string? Description { get; set; }
}