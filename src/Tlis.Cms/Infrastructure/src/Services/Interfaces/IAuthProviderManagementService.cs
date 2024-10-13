using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0.ManagementApi.Models;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface IAuthProviderManagementService
{
    ValueTask<string> CreateUserAsync(string email, string[] roleIds);

    ValueTask UpdateUserRolesAsync(string id, string[] roleIds);

    Task<List<Role>> GetAllRolesAsync();

    Task DeleteUserAsync(string id);
}