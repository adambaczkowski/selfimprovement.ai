using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromptApi.Services;

namespace LS.Common;

public static class ConfigureBlobStorage
{
    public static void AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBlobStorageService, BlobStorageService>();
        services.AddSingleton(_ => new BlobServiceClient(configuration["BlobStorageConnectionUrl"]));
    }
}