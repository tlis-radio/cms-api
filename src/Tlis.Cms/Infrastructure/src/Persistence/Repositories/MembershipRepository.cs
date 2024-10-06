using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class MembershipRepository(CmsDbContext context)
    : GenericRepository<Membership>(context), IMembershipRepository
{
    public Task<Guid?> GetIdByStatus(MembershipStatus status)
        => GetIdAsync(x => x.Status == status);

    public Task<List<Membership>> GetAll()
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), false);

        return query.ToListAsync();
    }
}