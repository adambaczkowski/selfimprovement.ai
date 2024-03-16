using Microsoft.Extensions.DependencyInjection;

namespace LS.Startup;

public static class WebApiExtensions
{
    public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services, string webHostUrl)
    {
        if (webHostUrl != null)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(webHostUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        return services;
    }
}