using System.Reflection;
using PromptApi.AI;
using PromptApi.AI.Prompts;
using PromptApi.Helpers;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class PromptBuilderService(IGoalApiClient goalApiClient, IIdentityApiClient identityApiClient) : IPromptBuilderService
{
    public async Task<string> CreatePrompt(Guid userId, Guid goalId)
    {
        // var goal = await goalApiClient.GetSingleGoal(new GetSingleGoalQuery()
        // {
        //     GoalId = goalId
        // });
        //
        // var user = await identityApiClient.GetUserDetails(new GetUserDetailsQuery()
        // {
        //     UserId = userId
        // });

        var prompt = LoadPrompt();
        
        return prompt;
    }

    private string LoadPrompt()
    {
        string test = "What is the capital city of %Country%";

        var testPrompt = new TestPrompt()
        {
            Country = "Poland",
            Age = "12",
        };

        var valuesDict = PromptRenderHelper.ConvertPromptValuesToDictonary(testPrompt);
        var prompt = PromptRenderHelper.Render(test, valuesDict);

        return prompt;
    }
}