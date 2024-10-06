using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.MembershipStatusGetAllResponses;

public sealed class MembershipStatusGetAllResponseItem
{
    public required Guid Id { get; set; }

    public required string Status { get; set; }
}