using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;

public sealed class BroadcastDetailsGetResponseShow
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}