using System.Collections.Generic;
using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetInDateRangeQuery;

public static class GetInDateRangeQueryMappings
{
    public static GetInDateRangeQueryResponse MapToResponse(this List<Broadcast> entities, ICloudeStorageService cloudeStorageService)
        => new()
        {
            Results = entities.Select(x => new GetInDateRangeQueryResponseBroadcast
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Show = new GetInDateRangeQueryResponseBroadcastShow
                {
                    Id = x.Show.Id,
                    Name = x.Show.Name
                },
                Image = x.Image is null ? null : new GetInDateRangeQueryResponseBroadcastImage
                {
                    Id = x.Image.Id,
                    Url = cloudeStorageService.GetBroadcastImageUrl(x.Image.FileName)
                }
            }).ToList()
        };
}