using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastUpdateRequest : IRequest<bool>
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required Guid ShowId { get; set; }
}