using System.Net;
using Newtonsoft.Json;

namespace LS.ServiceClient;

public abstract class BaseRestServiceClient
{
    private readonly IServiceClient _client;

    protected abstract string ServiceName { get; }

    protected BaseRestServiceClient(
        IAccessTokenProvider accessTokenProvider,
        IHttpClientFactory httpClientFactory)
    {
        _client = new RestClient(httpClientFactory, accessTokenProvider);
    }

    protected async Task<T[]?> Get<T>(string path, object? request = null, params Header[] headers)
    {
        var clientResponse = await _client.Get(ServiceName, path, request, headers);
        var clientResponseString = await clientResponse.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<T[]>(clientResponseString);
        return data;
    }

    protected async Task<T?> SingleGet<T>(string path, object? request = null, params Header[] headers)
    {
        var clientResponse = await _client.Get(ServiceName, path, request, headers);
        var clientResponseString = await clientResponse.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<T>(clientResponseString);
        return data;
    }

    protected async Task<T?> Post<T>(string path, object request, params Header[] headers)
    {
        var clientResponse = await _client.Post(ServiceName, path, request, headers);
        var clientResponseString = await clientResponse.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<T>(clientResponseString);
        return data;
    }

    protected async Task<Response<T>> PostWithResponse<T>(string path, object request, params Header[] headers)
    {
        var clientResponse = await _client.Post(ServiceName, path, request, headers);
        var result = new Response<T> { Status = clientResponse.StatusCode };
        if (result.Status == HttpStatusCode.OK)
        {
            var clientResponseString = await clientResponse.Content.ReadAsStringAsync();
            result.Result = JsonConvert.DeserializeObject<T>(clientResponseString);
        }

        return result;
    }

    protected async Task<Response> PostWithResponse(string path, object request, params Header[] headers)
    {
        var clientResponse = await _client.Post(ServiceName, path, request, headers);
        var result = new Response { Status = clientResponse.StatusCode };

        return result;
    }

    protected async Task Post(string path, object request, params Header[] headers)
    {
        await _client.Post(ServiceName, path, request, headers);
    }

    protected async Task Delete(string path, object request, params Header[] headers)
    {
        await _client.Delete(ServiceName, path, request, headers);
    }
}

public class Response
{
    public HttpStatusCode Status { get; set; }
}

public class Response<T> : Response
{
    public T? Result { get; set; }
}