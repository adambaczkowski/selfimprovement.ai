using GoalApi.Enums;
using LS.Common.Enums.Goal;
using LS.ServiceClient;

namespace PromptApi.ServiceClients;

public interface IGoalApiClient
{
    Task<GoalResource> GetSingleGoal(Guid goalId);
}

public class GetSingleGoalQuery
{
    public Guid GoalId { get; init; }
}

public class GoalResource
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public string Name { get; init; }
    public Goals GoalFriendlyName { get; init; }
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

    protected override string ServiceUrl => "host.docker.internal:8081";
    
    public async Task<GoalResource> GetSingleGoal(Guid goalId)
    {
        var url = $"{goalId}/Details";
        try
        {
            var result = await SingleGet<GoalResource>(url);
            return result;
        }
        catch (Exception ex)
        {
            var e = ex;
        }

        return default;
    }
    
    
}