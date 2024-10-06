using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses.RoleGetAllResponses;

public sealed class RoleGetAllResponse
{
    public required List<RoleGetAllResponseItem> Results { get; set; } = [];
}

