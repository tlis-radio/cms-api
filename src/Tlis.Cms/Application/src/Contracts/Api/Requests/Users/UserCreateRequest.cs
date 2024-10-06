using System;
using System.Collections.Generic;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Users;

public sealed class UserCreateRequest : IRequest<BaseCreateResponse>
{
    [SwaggerSchema(Description = "User's first name")]
    public required string Firstname { get; set; }

    [SwaggerSchema(Description = "User's last name")]
    public required string Lastname { get; set; }

    [SwaggerSchema(Description = "User's nickname or alias")]
    public required string Nickname { get; set; }

    [SwaggerSchema(Description = "If user prefers to show his nickname or name on main page")]
    public bool PreferNicknameOverName { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    public required string Abouth { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public string? Email { get; set; }

    public bool CmsAdminAccess { get; set; }

    [SwaggerSchema(Description = "User's role history.")]
    public required List<UserRoleHistoryCreateRequest> RoleHistory { get; set; } = [];

    [SwaggerSchema(Description = "User's membership history.")]
    public required List<UserMembershipHistoryCreateRequest> MembershipHistory { get; set; } = []; 
}

public sealed class UserRoleHistoryCreateRequest
{
    [SwaggerSchema(Description = "Id of the role.")]
    public Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The starting date of when user started this position.")]
    public DateTime FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public DateTime? FunctionEndDate { get; set; }

    [SwaggerSchema(Description = "Description why this position was given to user.")]
    public string? Description { get; set; }
}

public sealed class UserMembershipHistoryCreateRequest
{
    public MembershipStatus Status { get; set; }

    public DateTime ChangeDate { get; set; }

    public string Description { get; set; } = null!;
}