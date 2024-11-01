using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.Application.Contracts.Commands.Broadcasts.UpdateImageCommand;

public sealed class UpdateImageCommand : IRequest<bool>
{
    public required Guid Id { get; set; }

    public required IFormFile Image { get; set; }
}