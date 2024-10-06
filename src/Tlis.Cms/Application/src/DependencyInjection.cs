using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Services;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Infrastructure;

namespace Tlis.Cms.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services
            .AddOptions<ImageProcessingConfiguration>()
            .Bind(configuration.GetSection("ImageProcessing"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddScoped<IImageProcessingService, ImageProcessingService>();
        services.AddSingleton<IImageService, ImageService>();
    }
}