using LS.Common.Enums.Identity;
using LS.ServiceClient;

namespace PromptApi.ServiceClients;

public interface IIdentityApiClient
{
    Task<UserResource> GetUserDetails(string userId);
}

public class UserResource
{
    public Sex? Sex { get; init; }
    public int? Weight { get; init; }
    public int? Height { get; init; }
    public int? Age { get; init; }
    public Education? EducationLevel { get; init; }
}

public class IdentityApiClient : BaseRestServiceClient, IIdentityApiClient
{
    public IdentityApiClient(IAccessTokenProvider accessTokenProvider, IHttpClientFactory httpClientFactory) : base(accessTokenProvider, httpClientFactory)
    {
    }

    protected override string ServiceUrl => "host.docker.internal:8080";
    
    public async Task<UserResource> GetUserDetails(string userId)
    {
        var url = $"{userId}/Profile";
        return await SingleGet<UserResource>(url);
    }
    
    
}