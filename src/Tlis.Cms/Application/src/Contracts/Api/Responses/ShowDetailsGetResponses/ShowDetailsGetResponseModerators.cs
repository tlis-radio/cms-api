using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;

public class ShowDetailsGetResponseModerators
{
    public required Guid Id { get; set; }

    public required string Nickname { get; set; }
}