using System;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Mappings;

public static class BroadcastMappings
{
    public static Broadcast MapToBroadcast(BroadcastCreateRequest request)
    {
        return new Broadcast
        {
            Name = request.Name,
            ExternalUrl = string.Empty, // TODO: sem sa budu davat veci ako url na slido atd.
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ShowId = request.ShowId
        };
    }

    public static BroadcastDetailsGetResponse? MapToBroadcastDetailsGetResponse(Broadcast? entity)
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
            Image = entity.Image is null ? null : new BroadcastDetailsGetResponseImage
            {
                Id = entity.Image.Id,
                Url = entity.Image?.Url ?? string.Empty
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