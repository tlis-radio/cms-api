using System;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Mappings;

public static class BroadcastMappings
{
    public static BroadcastGetInDateRangeResponseBroadcast MapToBroadcastGetInDateRangeResponse(Broadcast entity, string? imageUrl)
    {
        ArgumentNullException.ThrowIfNull(entity.Show);

        var response =  new BroadcastGetInDateRangeResponseBroadcast
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Show = new BroadcastGetInDateRangeResponseBroadcastShow
            {
                Id = entity.Show.Id,
                Name = entity.Show.Name
            },
            Image = entity.Image is null ? null : new BroadcastGetInDateRangeResponseBroadcastImage
            {
                Id = entity.Image.Id,
                Url = imageUrl
            }
        };

        return response;
    }

    public static BroadcastDetailsGetResponse? MapToBroadcastDetailsGetResponse(Broadcast? entity, string? imageUrl)
    {
        if (entity is null)
        {
            return null;
        }

        ArgumentNullException.ThrowIfNull(entity.Show);

        var response =  new BroadcastDetailsGetResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Show = new BroadcastDetailsGetResponseShow
            {
                Id = entity.Show.Id,
                Name = entity.Show.Name
            },
            Image = entity.Image is null || imageUrl is null ? null : new BroadcastDetailsGetResponseImage
            {
                Id = entity.Image.Id,
                Url = imageUrl
            }
        };

        return response;
    }

    public static BroadcastPaginationGetResponse MapToBroadcastPaginationGetResponse(Broadcast entity)
    {
        return new BroadcastPaginationGetResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            ShowId = entity.ShowId
        };
    }
}