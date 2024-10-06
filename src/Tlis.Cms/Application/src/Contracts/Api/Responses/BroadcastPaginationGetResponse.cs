using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses;

public sealed class BroadcastPaginationGetResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required Guid ShowId { get; set; }
}