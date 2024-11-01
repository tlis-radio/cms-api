using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.Application.Contracts.Commands.User.UpdateProfileImageCommand;

public sealed class UpdateProfileImageCommand : IRequest<bool>
{
    public required Guid Id { get; set; }

    public required IFormFile ProfileImage { get; set; }
}