using System;
using System.Collections.Generic;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Shows;

public sealed class ShowCreateRequest : IRequest<BaseCreateResponse>
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required List<Guid> ModeratorIds { get; set; } = [];
}