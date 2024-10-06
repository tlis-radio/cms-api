using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

public sealed class UserGetResponseRole
{
    public required Guid Id { get; set; }

    public required string Name { get; set; } = null!;
}