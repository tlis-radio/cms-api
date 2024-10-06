using System;
using MediatR;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Shows;

public sealed class ShowDeleteRequest : IRequest<bool>
{
    public required Guid Id { get; set; }
}