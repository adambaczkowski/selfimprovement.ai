using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class PromptBuilderService(IGoalApiClient goalApiClient, IIdentityApiClient identityApiClient) : IPromptBuilderService
{
    public async Task<Prompt> CreatePrompt(Guid userId, Guid goalId)
    {
        var goal = await goalApiClient.GetSingleGoal(new GetSingleGoalQuery()
        {
            GoalId = goalId
        });

        var user = await identityApiClient.GetUserDetails(new GetUserDetailsQuery()
        {
            UserId = userId
        });
        
        
        
        return null;
    }
}