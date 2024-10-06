using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

public sealed class UserGetResponseUserRoleHistory
{
    public required Guid Id { get; set; }

    public required DateTime FunctionStartDate { get; set; }

    public required DateTime? FunctionEndDate { get; set; }

    public required UserGetResponseRole? Role { get; set; }

    public required string? Description { get; set; }
}