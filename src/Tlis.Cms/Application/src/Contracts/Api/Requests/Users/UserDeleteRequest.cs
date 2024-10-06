using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Users;

public sealed class UserDeleteRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}