using System;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Application.Contracts.Queries.Users.GetDetailsQuery;

public sealed class GetDetailsQueryResponseMembershipHistoryMembership
{
    public required Guid Id { get; set; }

    public required MembershipStatus Status { get; set; }
}