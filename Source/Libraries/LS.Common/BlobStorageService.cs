using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PromptApi.Services;

namespace LS.Common;

internal sealed class BlobStorageService(BlobServiceClient blobServiceClient) : IBlobStorageService
{
    private const string PromptContainerName = "prompts";
    private const string ProfileImagesContainerName = "profileimages";
    public async Task<string> DownloadPromptFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(PromptContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        
        return downloadResult.Content.ToString();
    }

    public async Task<byte[]> GetProfileImage(Guid profileImageId, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ProfileImagesContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(profileImageId.ToString());

        if (blobClient.ExistsAsync(cancellationToken).Result)
        {
            using var ms = new MemoryStream();
            await blobClient.DownloadToAsync(ms, cancellationToken);
            return ms.ToArray();
        }

        return null;
    }

    public async Task<Guid> UploadProfileImage(Stream stream, string contentType,
        CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ProfileImagesContainerName);

        var profileImageId = Guid.NewGuid();
        BlobClient blobClient = containerClient.GetBlobClient(profileImageId.ToString());

        await blobClient.UploadAsync(
            stream,
            new BlobHttpHeaders() { ContentType = contentType },
            cancellationToken: cancellationToken);

        return profileImageId;
    }
}