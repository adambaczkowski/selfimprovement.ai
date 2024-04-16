using System.Text;
using Newtonsoft.Json;

namespace LS.ServiceClient;

public static class RequestMessage
{
    public static HttpRequestMessage Get(string uri, IEnumerable<Header> headers)
    {
        var result = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri),
        };

        AddHeaders(result, headers);

        return result;
    }

    public static HttpRequestMessage Post(string uri, object? content, IEnumerable<Header> headers)
    {
        var result = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(uri),
            Content = Content(content),
        };

        AddHeaders(result, headers);

        return result;
    }

    public static HttpRequestMessage Delete(string uri, object? content, IEnumerable<Header> headers)
    {
        var result = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(uri),
            Content = Content(content),
        };

        AddHeaders(result, headers);

        return result;
    }

    public static HttpRequestMessage Put(string uri, object? content, IEnumerable<Header> headers)
    {
        var result = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(uri),
            Content = Content(content),
        };

        AddHeaders(result, headers);

        return result;
    }

    private static HttpContent Content(object? content)
    {
        return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
    }

    private static void AddHeaders(HttpRequestMessage request, IEnumerable<Header> headers)
    {
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Values);
        }
    }
}