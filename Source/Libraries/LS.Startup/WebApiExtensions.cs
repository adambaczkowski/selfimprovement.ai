using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using IdentityDbContext dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        
        dbContext.Database.Migrate();
    }
}