using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.Contracts.Queries.Shows.PaginationQuery;

public static class PaginationQueryMappings
{
    public static PaginationQueryResponse MapToResponse(this PaginationDto<Show> entities, ICloudeStorageService cloudeStorageService)
        => new()
        {
            Total = entities.Total,
            Limit = entities.Limit,
            Page = entities.Page,
            TotalPages = entities.TotalPages,
            Results = entities.Results.Select(x => MapToPaginationQueryResponseResult(x, cloudeStorageService)).ToList()
        };

    private static PaginationQueryResponseResult MapToPaginationQueryResponseResult(Show entity, ICloudeStorageService cloudeStorageService)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate,
            ModeratorNames = entity.Moderators.Select(x => $"{x.Firstname} {x.Lastname}").ToList(),
            ProfileImageUrl = cloudeStorageService.GetShowImageUrl(entity.ProfileImage?.FileName)
        };
}