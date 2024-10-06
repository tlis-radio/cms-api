using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class BroadcastRepository(CmsDbContext dbContext)
    : GenericRepository<Broadcast>(dbContext), IBroadcastRepository
{
    public async Task<PaginationDto<Broadcast>> PaginationAsync(int limit, int pageNumber)
    {
        var queryGetTotalCount = await ConfigureTracking(DbSet.AsQueryable(), false).CountAsync();
        
        var pageQuery = ConfigureTracking(DbSet.AsQueryable(), false);

        var page = await pageQuery
            .OrderBy(u => u.StartDate)
            .Skip(limit * (pageNumber - 1))
            .Take(limit)
            .ToListAsync();
        
        return new PaginationDto<Broadcast>
        {
            Total = queryGetTotalCount,
            Limit = limit,
            Page = pageNumber,
            Results = page
        };
    }
}