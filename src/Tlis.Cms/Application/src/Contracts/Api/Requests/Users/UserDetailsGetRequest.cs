using System;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Users;

public sealed class UserDetailsGetRequest : IRequest<UserGetResponse?>
{
    public Guid Id { get; set; }
}