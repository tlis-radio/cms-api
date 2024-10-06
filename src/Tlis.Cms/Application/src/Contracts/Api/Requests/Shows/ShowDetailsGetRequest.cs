using System;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;

namespace Tlis.Cms.Application.Contracts.Api.Requests.Shows;

public sealed class ShowDetailsGetRequest : IRequest<ShowDetailsGetResponse?>
{
    public required Guid Id { get; set; }
}