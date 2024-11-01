using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.Contracts.Queries.Shows.GetDetailsQuery;

public static class GetDetailsQueryMappings
{
    public static GetDetailsQueryResponse? MapToResponse(this Show? entity, ICloudeStorageService cloudeStorageService)
    {
        if (entity is null)
        {
            return null;
        }

        var profileImageUrl = cloudeStorageService.GetShowImageUrl(entity.ProfileImage?.FileName);

        return new GetDetailsQueryResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate,
            Moderators = entity.Moderators.Select(m =>
            {
                return new GetDetailsQueryResponseModerator
                {
                    Id = m.Id,
                    Nickname = m.Nickname
                };
            }).ToList(),
            ProfileImage = entity.ProfileImage is null || profileImageUrl is null ? null : new GetDetailsQueryResponseProfileImage
            {
                Id = entity.ProfileImage.Id,
                Url = profileImageUrl
            }
        };
    }
}