using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Commands.Users.DeleteCommand;

public sealed class DeleteCommand : IRequest<bool>
{
    public required Guid Id { get; set; }
}