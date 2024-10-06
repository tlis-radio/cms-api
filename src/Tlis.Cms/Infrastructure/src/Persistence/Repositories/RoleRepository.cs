using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class RoleRepository(CmsDbContext context)
    : GenericRepository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetByName(string name, bool asTracking)
        => (await GetAsync(x => x.Name == name, asTracking)).FirstOrDefault();

    public Task<List<Role>> GetAll()
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), false);

        return query.ToListAsync();
    }
}