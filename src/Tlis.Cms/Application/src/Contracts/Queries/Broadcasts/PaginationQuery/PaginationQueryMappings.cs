using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcasts.PaginationQuery;

public static class PaginationQueryMappings
{
    public static PaginationQueryResponse MapToResponse(this PaginationDto<Broadcast> pagination)
    {
        return new PaginationQueryResponse
        {
            Total = pagination.Total,
            Limit = pagination.Limit,
            Page = pagination.Page,
            TotalPages = pagination.TotalPages,
            Results = pagination.Results.Select(x => new PaginationQueryResponseResult
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                ShowId = x.ShowId
            }).ToList()
        };
    }
}