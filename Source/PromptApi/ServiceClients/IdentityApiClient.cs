using LS.ServiceClient;

namespace PromptApi.ServiceClients;

public interface IIdentityApiClient
{
    Task<UserResource> GetUserDetails(GetUserDetailsQuery query);
}

public class GetUserDetailsQuery
{
    public Guid UserId { get; init; }
}

public class UserResource
{
}

public class IdentityApiClient : BaseRestServiceClient, IIdentityApiClient
{
    public IdentityApiClient(IAccessTokenProvider accessTokenProvider, IHttpClientFactory httpClientFactory) : base(accessTokenProvider, httpClientFactory)
    {
    }

    protected override string ServiceName => "identity";
    
    public async Task<UserResource> GetUserDetails(GetUserDetailsQuery query)
    {
        return await SingleGet<UserResource>($"api/Identity", query);
    }
    
    
}