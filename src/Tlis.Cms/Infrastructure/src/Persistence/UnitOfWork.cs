using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Tlis.Cms.Infrastructure.Exceptions;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Persistence.Repositories;
using Tlis.Cms.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.Infrastructure.Persistence;

internal class UnitOfWork : IDisposable, IUnitOfWork
{
    public IUserRepository UserRepository => _lazyUserRepository.Value;

    public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

    public IMembershipRepository MembershipRepository => _lazyMembershipRepository.Value;

    public IUserRoleHistoryRepository UserRoleHistoryRepository => _lazyUserRoleHistoryRepository.Value;

    public IUserMembershipHistoryRepository UserMembershipHistoryRepository => _lazyUserMembershipHistoryRepository.Value;

    public IImageRepository ImageRepository => _lazyImageRepository.Value;

    public IShowRepository ShowRepository => _lazyShowRepository.Value;

    public IBroadcastRepository BroadcastRepository => _lazyBroadcastRepository.Value;

    private bool _disposed;

    private readonly ILogger<UnitOfWork> _logger;

    private readonly CmsDbContext _dbContext;

    private readonly Lazy<IUserRepository> _lazyUserRepository;

    private readonly Lazy<IRoleRepository> _lazyRoleRepository;

    private readonly Lazy<IMembershipRepository> _lazyMembershipRepository;

    private readonly Lazy<IUserRoleHistoryRepository> _lazyUserRoleHistoryRepository;

    private readonly Lazy<IUserMembershipHistoryRepository> _lazyUserMembershipHistoryRepository;

    private readonly Lazy<IImageRepository> _lazyImageRepository;

    private readonly Lazy<IShowRepository> _lazyShowRepository;

    private readonly Lazy<IBroadcastRepository> _lazyBroadcastRepository;

    public UnitOfWork(CmsDbContext dbContext, ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _lazyUserRepository = new(() => new UserRepository(_dbContext));
        _lazyRoleRepository = new(() => new RoleRepository(_dbContext));
        _lazyUserRoleHistoryRepository = new(() => new UserRoleHistoryRepository(_dbContext));
        _lazyUserMembershipHistoryRepository = new(() => new UserMembershipHistoryRepository(_dbContext));
        _lazyMembershipRepository = new(() => new MembershipRepository(_dbContext));
        _lazyImageRepository = new(() => new ImageRepository(_dbContext));
        _lazyShowRepository = new(() => new ShowRepository(_dbContext));
        _lazyBroadcastRepository = new(() => new BroadcastRepository(_dbContext));
        _logger = logger;
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError("{Exception}", exception.Message);
            
            throw exception switch
            {
                UniqueConstraintException => new EntityAlreadyExistsException(),
                DbUpdateConcurrencyException => new EntityNotFoundException(),
                _ => new Exception(exception.Message)
            };
        }
    }

    public async Task ExecutePendingMigrationsAsync()
    {
        var pendingMigrations = (await _dbContext.Database.GetPendingMigrationsAsync()).ToList();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation("Applying migrations: {Join}", string.Join(',', pendingMigrations));

            await _dbContext.Database.MigrateAsync();
        }
        else
        {
            _logger.LogInformation("No migrations to execute");
        }
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _dbContext.Database.BeginTransactionAsync();
    }

    public void Dispose()
    {
        if (!_disposed)
            _dbContext.Dispose();

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}