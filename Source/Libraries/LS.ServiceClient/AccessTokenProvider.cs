using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace LS.ServiceClient;

public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> Get()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        var token = request?.Headers["Authorization"];
        return Task.FromResult(token.HasValue && token.Value != StringValues.Empty ? token.ToString() !.Substring(7) : null);
    }
}