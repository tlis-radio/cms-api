using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Tlis.Cms.Infrastructure.Exceptions;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public IMembershipRepository MembershipRepository { get; }

    public IUserRoleHistoryRepository UserRoleHistoryRepository { get; }

    public IUserMembershipHistoryRepository UserMembershipHistoryRepository { get; }

    public IImageRepository ImageRepository { get; }

    public IShowRepository ShowRepository { get; }

    public IBroadcastRepository BroadcastRepository { get; }

    /// <exception cref="EntityAlreadyExistsException">Thrown when a unique constraint is violated</exception>
    /// <exception cref="ApiException">Thrown when an error occurs while saving changes</exception>
    public Task SaveChangesAsync();

    public Task ExecutePendingMigrationsAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();
}