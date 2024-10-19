using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;

public sealed class BroadcastDetailsGetResponseImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}