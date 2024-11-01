using System;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcast.GetDetailsQuery;

public sealed class GetDetailsQueryResponseShow
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}