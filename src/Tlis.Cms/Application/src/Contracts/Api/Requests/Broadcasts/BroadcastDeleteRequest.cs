using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastDeleteRequest : IRequest<bool>
{
    public required Guid Id { get; set; }
}