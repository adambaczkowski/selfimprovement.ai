using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LS.ServiceClient;

public static class Configuration
{
    public static void Register(this IServiceCollection container,
        IConfiguration configuration)
    {
        container.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();
        container.AddScoped<IServiceClient, RestClient>();
        container.AddHttpClient();
    }
}