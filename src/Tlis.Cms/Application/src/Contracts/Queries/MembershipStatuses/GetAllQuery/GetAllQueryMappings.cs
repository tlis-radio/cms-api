using System;
using System.Collections.Generic;
using System.Linq;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Queries.MembershipStatuses.GetAllQuery;

public static class GetAllQueryMappings
{
    public static GetAllQueryResponse MapToResponse(this List<Membership> memberships)
        => new()
        {
            Results = memberships.Select(membership => new GetAllQueryResponseItem
            {
                Id = membership.Id,
                Status = Enum.GetName(membership.Status) ?? throw new Exception($"Unable to Enum.GetName for {membership.Status}")
            }).ToList()
        };
}