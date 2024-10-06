using System;

namespace Tlis.Cms.Infrastructure.Persistence.Dtos;

public class UserWithOnlyNicknameDto
{
    public required Guid Id { get; set; }

    public required string Nickname { get; set; }
}