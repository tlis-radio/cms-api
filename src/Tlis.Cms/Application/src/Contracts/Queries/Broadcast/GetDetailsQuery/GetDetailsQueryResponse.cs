using System;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcast.GetDetailsQuery;

public sealed class GetDetailsQueryResponse
{
    public required Guid Id { get; set; }

    public required GetDetailsQueryResponseImage? Image { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required GetDetailsQueryResponseShow Show { get; set; }
}