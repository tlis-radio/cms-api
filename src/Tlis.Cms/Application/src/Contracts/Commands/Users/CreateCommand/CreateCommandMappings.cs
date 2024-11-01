using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Commands.Users.CreateCommand;

public static class CreateCommandMappings
{
    public static User MapToUser(this CreateCommand request)
    {
        return new User
        {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Nickname = request.Nickname,
            Email = request.Email,
            CmsAdminAccess = request.CmsAdminAccess,
            Abouth = request.Abouth,
            PreferNicknameOverName = request.PreferNicknameOverName
        };
    }
}