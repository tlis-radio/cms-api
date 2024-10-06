using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastPaginationGetRequest : IRequest<PaginationResponse<BroadcastPaginationGetResponse>>
{
    public required int Limit { get; set; }

    [Range(1, int.MaxValue)]
    public required int Page { get; set; }
}