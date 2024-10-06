using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.Infrastructure.Configurations;
using Tlis.Cms.Infrastructure.Persistence;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: oddelit
        services
            .AddOptions<ServiceUrlsConfiguration>()
            .Bind(configuration.GetSection("ServiceUrls"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IStorageService, StorageService>();

        //---

        services.AddDbContext(configuration);
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddAuthProviderManagementService(configuration);
    }

    public static void AddAuthProviderManagementService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<Auth0Configuration>()
            .Bind(configuration.GetSection("Auth0"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<ITokenProviderService, TokenProviderService>();
        services.AddScoped<IAuthProviderManagementService, AuthProviderManagementService>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ICmsDbContext, CmsDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, CmsDbContext.SCHEMA))
                    .UseSnakeCaseNamingConvention();
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
    }
}