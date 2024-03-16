using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace LS.Startup;

public static class HealthCheckExtensions
{
    public static void MapHealthChecks(this IApplicationBuilder app)
    {
        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health/ready");
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
            });
    }
}