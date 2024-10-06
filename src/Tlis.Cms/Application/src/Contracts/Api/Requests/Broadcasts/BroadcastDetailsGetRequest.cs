using System;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastDetailsGetRequest : IRequest<BroadcastDetailsGetResponse?>
{
    public required Guid Id { get; set; }
}