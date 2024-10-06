using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Infrastructure.Persistence.Dtos;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class ShowRepository(CmsDbContext context)
    : GenericRepository<Show>(context), IShowRepository
{
    public override Task<Show?> GetByIdAsync(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query
            .Include(x => x.Moderators)
            .Include(x => x.ProfileImage)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PaginationDto<Show>> PaginationAsync(int limit, int pageNumber)
    {
        var queryGetTotalCount = await ConfigureTracking(DbSet.AsQueryable(), false).CountAsync();
        
        var pageQuery = ConfigureTracking(DbSet.AsQueryable(), false);

        var includes = pageQuery
            .Include(x => x.Moderators)
            .Include(x => x.ProfileImage);

        var page = await includes
            .OrderBy(u => u.CreatedDate)
            .Skip(limit * (pageNumber - 1))
            .Take(limit)
            .ToListAsync();
        
        return new PaginationDto<Show>
        {
            Total = queryGetTotalCount,
            Limit = limit,
            Page = pageNumber,
            Results = page
        };
    }
}