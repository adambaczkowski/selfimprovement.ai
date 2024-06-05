namespace LS.ServiceClient;

public interface IServiceClient
{
    Task<HttpResponseMessage> Get(string serviceName, string path, object? request = null, params Header[] headers);

    Task<HttpResponseMessage> Post(string serviceName, string path, object request, string? externalApiKey, params Header[] headers);

    Task<HttpResponseMessage> Delete(string serviceName, string path, object request, params Header[] headers);

    Task<HttpResponseMessage> Put(string serviceName, string path, object? request, params Header[] headers);
}