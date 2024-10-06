using System;
using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses;

public class ShowPaginationGetResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required List<string> ModeratorNames { get; set; } = [];

    public required DateOnly CreatedDate { get; set; }

    public required string? ProfileImageUrl { get; set; }
}