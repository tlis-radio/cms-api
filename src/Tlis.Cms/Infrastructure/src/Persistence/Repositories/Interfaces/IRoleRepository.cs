using System.Collections.Generic;
using System.Threading.Tasks;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    public Task<Role?> GetByName(string name, bool asTracking);

    public Task<List<Role>> GetAll();
}