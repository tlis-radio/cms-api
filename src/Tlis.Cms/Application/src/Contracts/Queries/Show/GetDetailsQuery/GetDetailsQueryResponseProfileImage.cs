using System;

namespace Tlis.Cms.Application.Contracts.Queries.Show.GetDetailsQuery;

public sealed class GetDetailsQueryResponseProfileImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}