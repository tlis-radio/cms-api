using System;

namespace Tlis.Cms.Application.Contracts.Queries.User.GetDetailsQuery;

public sealed class GetDetailsQueryResponseMembershipHistory
{
    public required Guid Id { get; set; }

    public required GetDetailsQueryResponseMembershipHistoryMembership Membership { get; set; }

    public required DateTime ChangeDate { get; set; }

    public required string? Description { get; set; }
}