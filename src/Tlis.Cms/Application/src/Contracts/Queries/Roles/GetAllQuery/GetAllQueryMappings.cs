using System.Collections.Generic;
using System.Linq;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Queries.Roles.GetAllQuery;

public static class GetAllQueryMappings
{
    public static GetAllQueryResponse MapToResponse(this List<Role> entities)
        => new()
        {
            Results = entities.Select(x => new GetAllQueryResponseItem
            {
                Id = x.Id,
                Name = x.Name
            }).ToList()
        };
}