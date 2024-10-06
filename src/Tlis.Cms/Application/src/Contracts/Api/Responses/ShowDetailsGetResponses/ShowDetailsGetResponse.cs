using System;
using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;

public class ShowDetailsGetResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required List<ShowDetailsGetResponseModerators> Moderators { get; set; } = [];

    public required DateOnly CreatedDate { get; set; }

    public required ShowDetailsGetResponseImage? ProfileImage { get; set; }
}