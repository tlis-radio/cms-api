using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Shows;

public sealed class ShowUpdateProfileImageRequest : IRequest<bool>
{
    public required Guid Id { get; set; }

    public required IFormFile ProfileImage { get; set; }
}