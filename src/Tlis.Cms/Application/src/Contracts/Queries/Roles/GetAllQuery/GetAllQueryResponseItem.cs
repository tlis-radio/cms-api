using System;

namespace Tlis.Cms.Application.Contracts.Queries.Roles.GetAllQuery;

public sealed class GetAllQueryResponseItem
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}