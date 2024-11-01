using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Commands.Users.CreateCommand;

public static class CreateCommandMappings
{
    public static User MapToUser(this CreateCommand command)
        => new()
        {
            Firstname = command.Firstname,
            Lastname = command.Lastname,
            Nickname = command.Nickname,
            Email = command.Email,
            CmsAdminAccess = command.CmsAdminAccess,
            Abouth = command.Abouth,
            PreferNicknameOverName = command.PreferNicknameOverName
        };
}