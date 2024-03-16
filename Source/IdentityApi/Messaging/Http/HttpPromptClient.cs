namespace IdentityApi.Messaging.Http;

public class HttpPromptClient : IPromptClient
{
    private readonly HttpClient _httpClient;

    public HttpPromptClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}