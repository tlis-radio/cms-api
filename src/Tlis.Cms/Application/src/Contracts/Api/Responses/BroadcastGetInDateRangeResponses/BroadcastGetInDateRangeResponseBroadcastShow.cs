using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;

public sealed class BroadcastGetInDateRangeResponseBroadcastShow
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}