using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.ResourceDetectors.Container;
using OpenTelemetry.ResourceDetectors.Host;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tlis.Cms.Shared;

namespace Tlis.Cms.Api.Extensions;

internal static class OtelSetup
{
    public static void ConfigureOtel(this IServiceCollection services, IHostEnvironment environment)
    {
        var deploymentEnvironmentAttribute = new KeyValuePair<string, object>("deployment.environment", environment.EnvironmentName);

        services
            .AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .SetResourceBuilder(ResourceBuilder.CreateDefault())
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddOtlpExporter())
            .WithTracing(tracing => tracing
                .SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddDetector(new ContainerResourceDetector())
                    .AddDetector(new HostDetector()))
                .AddSource(Telemetry.ServiceName)
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddNpgsql()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter()
                .AddOtlpExporter());
    }

    public static void ConfigureOtel(this ILoggingBuilder logging, IHostEnvironment environment)
    {
        var deploymentEnvironmentAttribute = new KeyValuePair<string, object>("deployment.environment", environment.EnvironmentName);

        logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(ResourceBuilder.CreateDefault())
                .AddOtlpExporter();
        });
    }
}