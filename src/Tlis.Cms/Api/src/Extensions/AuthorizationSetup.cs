using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tlis.Cms.Api.Constants;

namespace Tlis.Cms.Api.Extensions;

internal static class AuthorizationSetup
{
    public static void ConfigureAuthorization(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetSection("Jwt").GetValue<string>("Authority");
                options.Audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
                options.RequireHttpsMetadata = configuration.GetSection("Jwt").GetValue<bool>("RequireHttpsMetadata");
                options.SaveToken = true;
            });
        
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.UserWrite, policy => policy.RequireClaim("permissions", "write:user"))
            .AddPolicy(Policy.UserDelete, policy => policy.RequireClaim("permissions", "delete:user"))
            .AddPolicy(Policy.UserRead, policy => policy.RequireClaim("permissions", "read:user"))
            .AddPolicy(Policy.ShowWrite, policy => policy.RequireClaim("permissions", "write:show"))
            .AddPolicy(Policy.ShowDelete, policy => policy.RequireClaim("permissions", "delete:show"))
            .AddPolicy(Policy.ShowRead, policy => policy.RequireClaim("permissions", "read:show"))
            .AddPolicy(Policy.BroadcastWrite, policy => policy.RequireClaim("permissions", "write:program"))
            .AddPolicy(Policy.BroadcastDelete, policy => policy.RequireClaim("permissions", "delete:program"))
            .AddPolicy(Policy.BroadcastRead, policy => policy.RequireClaim("permissions", "read:program"))
            .AddPolicy(Policy.ImageWrite, policy => policy.RequireClaim("permissions", "write:image"))
            .AddPolicy(Policy.ImageDelete, policy => policy.RequireClaim("permissions", "delete:image"))
            .AddPolicy(Policy.ImageRead, policy => policy.RequireClaim("permissions", "read:image"));
    }
}