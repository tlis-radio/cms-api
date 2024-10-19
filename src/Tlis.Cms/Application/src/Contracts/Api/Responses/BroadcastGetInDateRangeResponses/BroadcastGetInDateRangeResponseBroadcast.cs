using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;

public sealed class BroadcastGetInDateRangeResponseBroadcast
{
    public required Guid Id { get; set; }

    public required BroadcastGetInDateRangeResponseBroadcastImage? Image { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required BroadcastGetInDateRangeResponseBroadcastShow Show { get; set; }
}