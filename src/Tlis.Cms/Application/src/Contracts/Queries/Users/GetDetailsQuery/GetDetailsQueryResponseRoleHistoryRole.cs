using System;

namespace Tlis.Cms.Application.Contracts.Queries.Users.GetDetailsQuery;

public sealed class GetDetailsQueryResponseRoleHistoryRole
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}