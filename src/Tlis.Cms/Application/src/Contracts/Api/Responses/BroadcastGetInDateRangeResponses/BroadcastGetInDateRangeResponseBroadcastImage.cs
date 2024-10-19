using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;

public sealed class BroadcastGetInDateRangeResponseBroadcastImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}