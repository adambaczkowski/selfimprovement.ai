namespace PromptApi.Services;

public interface IBlobStorageService
{
    Task<string> DownloadTextFileAsync(string fileName, CancellationToken cancellationToken = default);
}