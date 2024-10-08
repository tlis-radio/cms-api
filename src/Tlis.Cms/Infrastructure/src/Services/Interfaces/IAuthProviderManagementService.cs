using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0.ManagementApi.Models;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface IAuthProviderManagementService
{
    ValueTask<string> CreateUser(string email, string[] roleIds);

    ValueTask UpdateUserRoles(string id, string[] roleIds);

    Task<List<Role>> GetAllRoles();

    Task DeleteUser(string id);
}