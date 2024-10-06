using System;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastCreateRequest : IRequest<BaseCreateResponse>
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required Guid ShowId { get; set; }
}