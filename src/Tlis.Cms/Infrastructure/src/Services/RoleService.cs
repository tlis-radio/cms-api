using System;
using System.Threading.Tasks;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Services;

public class RoleService(IUnitOfWork unitOfWork, ICacheService cacheService) : IRoleService
{
    public async Task<Role?> GetByIdAsync(Guid id)
    {
        var cacheKey = CreateRoleCacheKey(id);
        if (cacheService.TryGetValue<Role>(cacheKey, out var role) is false)
        {
            role = await unitOfWork.RoleRepository.GetByIdAsync(id, asTracking: false);
            if (role is null)
                return null;

            cacheService.Set(cacheKey, role);
            return role;
        }

        return role;
    }

    private static string CreateRoleCacheKey(Guid id) => $"role-{id}";
}