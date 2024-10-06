using System;
using System.Linq;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Application.Mappings;

public static class ShowMappings
{
    public static ShowPaginationGetResponse ToPaginationDto(Show entity)
    {
        return new ShowPaginationGetResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate,
            ModeratorNames = entity.Moderators.Select(x => $"{x.Firstname} {x.Lastname}").ToList(),
            ProfileImageUrl = entity.ProfileImage?.Url
        };
    }

    public static ShowDetailsGetResponse ToDto(Show entity)
    {
        var response = new ShowDetailsGetResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate,
            Moderators = entity.Moderators.Select(m =>
            {
                return new ShowDetailsGetResponseModerators
                {
                    Id = m.Id,
                    Nickname = m.Nickname
                };
            }).ToList(),
            ProfileImage = entity.ProfileImage is null ? null : new ShowDetailsGetResponseImage
            {
                Id = entity.ProfileImage.Id,
                Url = entity.ProfileImage.Url
            }
        };

        return response;
    }

    public static Show ToEntity(ShowCreateRequest request)
    {
        return new Show
        {
            Name = request.Name,
            Description = request.Description,
            CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),
            ShowsUsers = request.ModeratorIds.Select(x => new ShowsUsers { UserId = x }).ToList()
        };
    }
}