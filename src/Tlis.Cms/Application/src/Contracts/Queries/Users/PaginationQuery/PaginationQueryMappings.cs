using System;
using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;

namespace Tlis.Cms.Application.Contracts.Queries.Users.PaginationQuery;

public static class PaginationQueryMappings
{
    public static PaginationQueryResponse MapToResponse(this PaginationDto<User> entities)
        => new()
        {
            Total = entities.Total,
            Limit = entities.Limit,
            Page = entities.Page,
            TotalPages = entities.TotalPages,
            Results = entities.Results.Select(MapToPaginationQueryResponseResult).ToList()
        };

    private static PaginationQueryResponseResult MapToPaginationQueryResponseResult(User user)
    {
        var latestMembership = user.MembershipHistory.OrderByDescending(x => x.ChangeDate).FirstOrDefault();

        return new PaginationQueryResponseResult
        {
            Id = user.Id,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Nickname = user.Nickname,
            Email = user.Email,
            CmsAdminAccess = user.CmsAdminAccess,
            Roles = user.RoleHistory.Select(x => x.Role!.Name).ToList(),
            Status = latestMembership != null && latestMembership.Membership != null
                ? Enum.GetName(latestMembership.Membership.Status)
                : null
        };
    }
}