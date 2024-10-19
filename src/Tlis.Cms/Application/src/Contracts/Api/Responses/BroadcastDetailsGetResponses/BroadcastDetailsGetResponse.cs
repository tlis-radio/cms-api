using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;

public sealed class BroadcastDetailsGetResponse
{
    public required Guid Id { get; set; }

    public required BroadcastDetailsGetResponseImage? Image { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required BroadcastDetailsGetResponseShow Show { get; set; }
}