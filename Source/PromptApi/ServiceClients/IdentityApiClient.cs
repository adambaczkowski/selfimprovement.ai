using LS.Common.Enums.Identity;
using LS.ServiceClient;

namespace PromptApi.ServiceClients;

public interface IIdentityApiClient
{
    Task<UserResource> GetUserDetails(GetUserDetailsQuery query);
}

public class GetUserDetailsQuery
{
    public string UserId { get; init; }
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

    protected override string ServiceName => "identity";
    
    public async Task<UserResource> GetUserDetails(GetUserDetailsQuery query)
    {
        return await SingleGet<UserResource>($"api/Identity", query);
    }
    
    
}