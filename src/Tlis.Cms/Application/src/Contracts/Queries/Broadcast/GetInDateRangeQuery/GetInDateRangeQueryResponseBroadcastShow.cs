using System;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcast.GetInDateRangeQuery;

public sealed class GetInDateRangeQueryResponseBroadcastShow
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}