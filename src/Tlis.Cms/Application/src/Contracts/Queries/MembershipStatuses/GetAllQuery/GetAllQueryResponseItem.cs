using System;

namespace Tlis.Cms.Application.Contracts.Queries.MembershipStatuses.GetAllQuery;

public sealed class GetAllQueryResponseItem
{
    public required Guid Id { get; set; }

    public required string Status { get; set; }
}