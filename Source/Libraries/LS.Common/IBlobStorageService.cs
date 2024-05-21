namespace PromptApi.Services;

public interface IBlobStorageService
{
    Task<string> DownloadPromptFileAsync(string fileName, CancellationToken cancellationToken = default);

    Task<Guid> UploadProfileImage(Stream stream, string contentType, CancellationToken cancellationToken = default);
    Task<byte[]> GetProfileImage(Guid profileImageId, CancellationToken cancellationToken = default);
}