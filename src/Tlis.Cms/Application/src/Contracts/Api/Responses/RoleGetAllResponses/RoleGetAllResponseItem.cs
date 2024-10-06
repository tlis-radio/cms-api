using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.RoleGetAllResponses;

public sealed class RoleGetAllResponseItem
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}