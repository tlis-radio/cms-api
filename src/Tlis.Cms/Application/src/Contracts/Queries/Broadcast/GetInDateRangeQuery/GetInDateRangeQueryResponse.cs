using System;
using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcast.GetInDateRangeQuery;

public class GetInDateRangeQueryResponse
{
    public required List<GetInDateRangeQueryResponseBroadcast> Results { get; set; }
}