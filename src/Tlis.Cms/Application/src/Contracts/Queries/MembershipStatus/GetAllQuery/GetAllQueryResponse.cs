using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Queries.MembershipStatuses.GetAllQuery;

public sealed class GetAllQueryResponse
{
    public required List<GetAllQueryResponseItem> Results { get; set; } = [];
}