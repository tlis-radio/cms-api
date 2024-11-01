using System;
using System.Collections.Generic;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Base;

namespace Tlis.Cms.Application.Contracts.Commands.Shows.CreateCommand;

public sealed class CreateCommand : IRequest<BaseCreateResponse>
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required List<Guid> ModeratorIds { get; set; } = [];
}