using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.Contracts.Queries.Shows.PaginationQuery;

public static class PaginationQueryMappings
{
    public static PaginationQueryResponse MapToResponse(this PaginationDto<Show> entities, ICloudeStorageService cloudeStorageService)
    {
        return new PaginationQueryResponse
        {
            Total = entities.Total,
            Limit = entities.Limit,
            Page = entities.Page,
            TotalPages = entities.TotalPages,
            Results = entities.Results.Select(x => new PaginationQueryResponseResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    ModeratorNames = x.Moderators.Select(x => $"{x.Firstname} {x.Lastname}").ToList(),
                    ProfileImageUrl = cloudeStorageService.GetShowImageUrl(x.ProfileImage?.FileName)
                }).ToList()
        };
    }
}