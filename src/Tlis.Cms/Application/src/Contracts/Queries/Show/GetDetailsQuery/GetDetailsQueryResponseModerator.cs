using System;

namespace Tlis.Cms.Application.Contracts.Queries.Show.GetDetailsQuery;

public sealed class GetDetailsQueryResponseModerator
{
    public required Guid Id { get; set; }

    public required string Nickname { get; set; }
}