using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence;

internal class CmsDbContext(DbContextOptions options) : DbContext(options), ICmsDbContext
{
    public DbSet<Role> Roles { get; set; }

    public readonly static string SCHEMA = "cms";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CmsDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}