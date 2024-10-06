using Tlis.Cms.Application.Contracts.Api.Responses.RoleGetAllResponses;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Mappings;

internal static class RoleMappings
{
    public static RoleGetAllResponseItem ToRoleGetAllResponseItem(Role dto)
    {
        return new RoleGetAllResponseItem
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}