using System;
using System.Collections.Generic;

namespace Tlis.Cms.Application.Contracts.Api.Responses;

public sealed class UserPaginationGetResponse
{
    public required Guid Id { get; set; }

    public required bool CmsAdminAccess { get; set; }

    public required string Firstname { get; set; }

    public required string Lastname { get; set; }

    public required string Nickname { get; set; }

    public required string? Email { get; set; }

    public required List<string> Roles { get; set; } = [];

    public required string? Status { get; set; }
}