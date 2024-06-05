using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;

namespace LS.ServiceClient;

public class RestClient : IServiceClient
{
    private HttpClient Client
    {
        get
        {
            var client = _httpClientFactory.CreateClient(nameof(RestClient));
            client.Timeout = TimeSpan.FromMinutes(20);
            return client;
        }
    }

    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryOnServerError = Policy
        .HandleResult<HttpResponseMessage>(x => x.StatusCode.ToString().StartsWith("5"))
        .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(100));

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAccessTokenProvider _accessTokenProvider;

    public RestClient(
        IHttpClientFactory httpClientFactory,
        IAccessTokenProvider accessTokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _accessTokenProvider = accessTokenProvider;
    }

    public async Task<HttpResponseMessage> Get(string serviceName, string path, object request = null,
        params Header[] headers)
    {
        var baseAddress = GetBaseAddress(serviceName);
        var queryString = request != null
            ? await ToQueryString(request)
            : string.Empty;

        var requestMessage = RequestMessage.Get(
            $"{baseAddress}{path}?{queryString}".TrimEnd('?'),
            (headers ?? Array.Empty<Header>()));

        await AddAuthorizationBearerToken(requestMessage);
        return await _retryOnServerError.ExecuteAsync(() => Client.SendAsync(requestMessage));
    }

    public async Task<HttpResponseMessage> Post(string serviceName, string path, object request, string? externalApiKey,
        params Header[] headers)
    {
        var baseAddress = GetBaseAddress(serviceName, !String.IsNullOrEmpty(externalApiKey));

        var requestMessage = RequestMessage.Post(
            $"{baseAddress}{path}",
            request,
            (headers ?? Array.Empty<Header>()));

        await AddAuthorizationBearerToken(requestMessage);

        return await _retryOnServerError.ExecuteAsync(() => Client.SendAsync(requestMessage));
    }

    public async Task<HttpResponseMessage> Delete(string serviceName, string path, object request,
        params Header[] headers)
    {
        var baseAddress = GetBaseAddress(serviceName);

        var requestMessage = RequestMessage.Delete(
            $"{baseAddress}{path}",
            request,
            (headers ?? Array.Empty<Header>()));

        await AddAuthorizationBearerToken(requestMessage);

        return await _retryOnServerError.ExecuteAsync(() => Client.SendAsync(requestMessage));
    }

    public async Task<HttpResponseMessage> Put(string serviceName, string path, object request,
        params Header[] headers)
    {
        var baseAddress = GetBaseAddress(serviceName);

        var requestMessage = RequestMessage.Put(
            $"{baseAddress}{path}",
            request,
            (headers ?? Array.Empty<Header>()));

        await AddAuthorizationBearerToken(requestMessage);
        return await _retryOnServerError.ExecuteAsync(() => Client.SendAsync(requestMessage));
    }

    private string GetBaseAddress(string service, bool isExternal = false)
    {
        var resolvedUrl = isExternal ? $"https://{service}" : $"http://{service}";
        resolvedUrl = resolvedUrl.TrimEnd('/') + "/";
        return resolvedUrl;
    }

    private async Task AddAuthorizationBearerToken(HttpRequestMessage httpRequest, string? externalApiKey = null)
    {
        if (externalApiKey is not null)
        {
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{externalApiKey}");
        }
        else
        {
            var usersAccessToken = await _accessTokenProvider.Get();
            if (usersAccessToken != null)
            {
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{usersAccessToken}");
            }
        }
    }

    private async Task<string> ToQueryString(object request)
    {
        var keyValueContent = ToKeyValue(request);
        var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
        return await formUrlEncodedContent.ReadAsStringAsync();
    }

    private static IDictionary<string, string>? ToKeyValue(object metaToken)
    {
        if (metaToken == null)
        {
            return null;
        }

        JToken token = metaToken as JToken;
        if (token == null)
        {
            return ToKeyValue(JObject.FromObject(metaToken));
        }

        if (token.HasValues)
        {
            var contentData = new Dictionary<string, string>();
            foreach (var child in token.Children().ToList())
            {
                var childContent = ToKeyValue(child);
                if (childContent != null)
                {
                    contentData = contentData.Concat(childContent)
                        .ToDictionary(k => k.Key, v => v.Value);
                }
            }

            return contentData;
        }

        var jValue = token as JValue;
        if (jValue?.Value == null)
        {
            return null;
        }

        var value = jValue.Type == JTokenType.Date
            ? jValue.ToString("o", CultureInfo.InvariantCulture)
            : jValue.ToString(CultureInfo.InvariantCulture);

        return new Dictionary<string, string> { { token.Path, value } };
    }
}