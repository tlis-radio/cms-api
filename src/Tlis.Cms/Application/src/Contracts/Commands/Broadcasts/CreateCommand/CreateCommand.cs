using System;
using MediatR;
using Tlis.Cms.Application.Contracts.Commands.Base;

namespace Tlis.Cms.Application.Contracts.Commands.Broadcasts.CreateCommand;

public sealed class CreateCommand : IRequest<BaseCreateResponse>
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required Guid ShowId { get; set; }
}