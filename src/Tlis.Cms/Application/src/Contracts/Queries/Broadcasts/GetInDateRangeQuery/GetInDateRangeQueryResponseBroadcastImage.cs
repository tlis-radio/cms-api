using System;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetInDateRangeQuery;

public sealed class GetInDateRangeQueryResponseBroadcastImage
{
    public required Guid Id { get; set; }

    public required string? Url { get; set; }
}