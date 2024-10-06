using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Shows;

public sealed class ShowUpdateRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required List<Guid> ModeratorIds { get; set; } = [];
}