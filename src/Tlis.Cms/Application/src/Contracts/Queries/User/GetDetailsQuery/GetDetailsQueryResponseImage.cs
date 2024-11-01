using System;

namespace Tlis.Cms.Application.Contracts.Queries.User.GetDetailsQuery;

public sealed class GetDetailsQueryResponseImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}