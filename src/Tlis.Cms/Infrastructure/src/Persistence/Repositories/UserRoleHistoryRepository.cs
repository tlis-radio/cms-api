using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence.Repositories;

internal sealed class UserRoleHistoryRepository(CmsDbContext context)
    : GenericRepository<UserRoleHistory>(context), IUserRoleHistoryRepository;