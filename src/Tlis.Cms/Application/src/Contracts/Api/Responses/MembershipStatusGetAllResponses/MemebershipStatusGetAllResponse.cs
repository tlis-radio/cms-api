using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses.MembershipStatusGetAllResponses;

public sealed class MembershipStatusGetAllResponse
{
    public required List<MembershipStatusGetAllResponseItem> Results { get; set; } = [];
}

