using System;
using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Queries.Show.GetDetailsQuery;

public sealed class GetDetailsQueryResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required List<GetDetailsQueryResponseModerator> Moderators { get; set; } = [];

    public required DateOnly CreatedDate { get; set; }

    public required GetDetailsQueryResponseProfileImage? ProfileImage { get; set; }
}