using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class UserMembershipHistoryRepository(CmsDbContext context)
    : GenericRepository<UserMembershipHistory>(context), IUserMembershipHistoryRepository;