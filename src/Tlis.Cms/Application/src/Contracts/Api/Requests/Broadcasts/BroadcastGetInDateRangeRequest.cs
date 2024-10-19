using System;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastGetInDateRangeRequest : IRequest<BroadcastGetInDateRangeResponse?>
{
    public required DateTime From { get; set; }

    public required DateTime To { get; set; }
}