using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

public interface IMembershipRepository : IGenericRepository<Membership>
{
    Task<Guid?> GetIdByStatus(MembershipStatus status);

    Task<List<Membership>> GetAll();
}