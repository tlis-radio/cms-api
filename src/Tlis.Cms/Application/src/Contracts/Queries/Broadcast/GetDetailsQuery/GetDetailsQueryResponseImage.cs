using System;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcast.GetDetailsQuery;

public sealed class GetDetailsQueryResponseImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}