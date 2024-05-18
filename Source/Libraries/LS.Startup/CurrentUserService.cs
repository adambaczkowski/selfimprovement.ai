using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LS.Startup;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User?.Claims.First(i => i.Type == "UserId").Value;
}