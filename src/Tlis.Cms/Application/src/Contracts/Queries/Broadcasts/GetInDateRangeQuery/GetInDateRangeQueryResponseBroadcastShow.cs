using System;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetInDateRangeQuery;

public sealed class GetInDateRangeQueryResponseBroadcastShow
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}