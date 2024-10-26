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
    public static ShowPaginationGetResponse MapToShowPaginationGetResponse(Show entity, string? profileImageUrl)
    {
        return new ShowPaginationGetResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate,
            ModeratorNames = entity.Moderators.Select(x => $"{x.Firstname} {x.Lastname}").ToList(),
            ProfileImageUrl = profileImageUrl
        };
    }

    public static ShowDetailsGetResponse MapToShowDetailsGetResponse(Show entity, string? profileImageUrl)
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
            ProfileImage = entity.ProfileImage is null || profileImageUrl is null ? null : new ShowDetailsGetResponseImage
            {
                Id = entity.ProfileImage.Id,
                Url = profileImageUrl
            }
        };

        return response;
    }

    public static Show MapToShow(ShowCreateRequest request)
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