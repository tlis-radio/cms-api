using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

public sealed class UserGetResponseUserMembershipHistory
{
    public required Guid Id { get; set; }

    public required UserGetResponseUserMembershipHistoryMembership Membership { get; set; }

    public required DateTime ChangeDate { get; set; }

    public required string? Description { get; set; }
}