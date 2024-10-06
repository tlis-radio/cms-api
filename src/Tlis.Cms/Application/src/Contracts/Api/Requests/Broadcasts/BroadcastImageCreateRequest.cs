using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.Application.Contracts.Api.Responses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastImageCreateRequest : IRequest<BaseCreateResponse>
{
    public required Guid BroadcastId { get; set; }

    public required IFormFile Image { get; set; }
}