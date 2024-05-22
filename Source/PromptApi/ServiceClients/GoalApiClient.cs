using GoalApi.Enums;
using LS.Common.Enums.Goal;
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
    public Guid Id { get; init; }
    public GoalCategories Category { get; init; }
    public UserAdvancement UserAdvancement { get; init; }
    public TimeAvailabilityPerDay TimeAvailabilityPerDay { get; init; }
    public TimeAvailabilityPerWeek TimeAvailabilityPerWeek { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Experience Experience { get; init; }
    
    public LearningType LearningType { get; init; }
    public string UserInput { get; init; }
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