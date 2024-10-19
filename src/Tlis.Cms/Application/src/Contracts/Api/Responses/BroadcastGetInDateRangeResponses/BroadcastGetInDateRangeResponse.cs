using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;

public sealed class BroadcastGetInDateRangeResponse
{
    public required List<BroadcastGetInDateRangeResponseBroadcast> Results { get; set; }
}