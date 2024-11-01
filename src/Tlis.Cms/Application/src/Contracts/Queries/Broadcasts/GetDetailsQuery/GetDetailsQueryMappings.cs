using System;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Queries.Broadcasts.GetDetailsQuery;

public static class GetDetailsQueryMappings
{
    public static GetDetailsQueryResponse? MapToResponse(this Broadcast? entity, string? imageUrl)
    {
        if (entity is null)
        {
            return null;
        }

        ArgumentNullException.ThrowIfNull(entity.Show);

        return new GetDetailsQueryResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Show = new GetDetailsQueryResponseShow
            {
                Id = entity.Show.Id,
                Name = entity.Show.Name
            },
            Image = entity.Image is null || imageUrl is null ? null : new GetDetailsQueryResponseImage
            {
                Id = entity.Image.Id,
                Url = imageUrl
            }
        };
    }
}