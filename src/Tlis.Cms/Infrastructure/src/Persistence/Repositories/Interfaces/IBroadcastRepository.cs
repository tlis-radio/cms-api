using System.Threading.Tasks;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

public interface IBroadcastRepository : IGenericRepository<Broadcast>
{
    Task<PaginationDto<Broadcast>> PaginationAsync(int limit, int pageNumber);
}