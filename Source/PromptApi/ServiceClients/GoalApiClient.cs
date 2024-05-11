using LS.ServiceClient;

namespace PromptApi.ServiceClients;

public interface IGoalApiClient
{
    Task<GoalResource> GetSingleGoal(GetSingleGoalQuery query);
}

public class GetSingleGoalQuery
{
    public Guid GoalId { get; init; }
}

public class GoalResource
{
}

public class GoalApiClient : BaseRestServiceClient, IGoalApiClient
{
    public GoalApiClient(IAccessTokenProvider accessTokenProvider, IHttpClientFactory httpClientFactory) : base(accessTokenProvider, httpClientFactory)
    {
    }

    protected override string ServiceName => "goal";
    
    public async Task<GoalResource> GetSingleGoal(GetSingleGoalQuery query)
    {
        return await SingleGet<GoalResource>($"api/Goal", query);
    }
    
    
}