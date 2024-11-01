using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcasts.PaginationQuery;

public static class PaginationQueryMappings
{
    public static PaginationQueryResponse MapToResponse(this PaginationDto<Broadcast> pagination)
        => new()
        {
            Total = pagination.Total,
            Limit = pagination.Limit,
            Page = pagination.Page,
            TotalPages = pagination.TotalPages,
            Results = pagination.Results.Select(MapToPaginationQueryResponseResult).ToList()
        };

    private static PaginationQueryResponseResult MapToPaginationQueryResponseResult(this Broadcast broadcast)
        => new()
        {
            Id = broadcast.Id,
            Name = broadcast.Name,
            Description = broadcast.Description,
            StartDate = broadcast.StartDate,
            EndDate = broadcast.EndDate,
            ShowId = broadcast.ShowId
        };
}