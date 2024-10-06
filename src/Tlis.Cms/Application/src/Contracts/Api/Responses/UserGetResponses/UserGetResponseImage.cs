using System;

namespace Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

public sealed class UserGetResponseImage
{
    public required Guid Id { get; set; }

    public required string Url { get; set; }
}