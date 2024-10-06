using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;

public class ShowDetailsGetResponseImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}