using System;

namespace Tlis.Cms.Application.Contracts.Queries.User.GetDetailsQuery;

public sealed class GetDetailsQueryResponseRoleHistory
{
    public required Guid Id { get; set; }

    public required DateTime FunctionStartDate { get; set; }

    public required DateTime? FunctionEndDate { get; set; }

    public required GetDetailsQueryResponseRoleHistoryRole? Role { get; set; }

    public required string? Description { get; set; }
}