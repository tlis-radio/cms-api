using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;

public sealed class BroadcastUpdateImageRequest : IRequest<bool>
{
    public required Guid Id { get; set; }

    public required IFormFile Image { get; set; }
}