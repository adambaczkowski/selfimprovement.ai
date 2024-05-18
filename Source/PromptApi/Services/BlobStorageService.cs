using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace PromptApi.Services;

internal sealed class BlobStorageService(BlobServiceClient blobServiceClient) : IBlobStorageService
{
    private const string ContainerName = "prompts";
    public async Task<string> DownloadTextFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        
        return downloadResult.Content.ToString();
    }
}