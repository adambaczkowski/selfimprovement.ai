using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.ServiceClients;
using PromptApi.Services;

namespace PromptApi.AI.Zephyr;

public class ZephyrModel(
    string name,
    string apiUrl,
    IPromptBuilderService promptBuilderService)
    : IAiModel
{
    public string Name { get; init; } = name;
    public string ApiUrl { get; init; } = apiUrl;
    public AiModelName AiModelName { get; } = AiModelName.Zephyr;
    public IAiRequestModel RequestModel { get; set; }

    public async Task<string> BuildPrompt(string userId,Guid goalId)
    {
        return await promptBuilderService.CreatePrompt(userId, goalId);
    }
    
    public async Task<AiResponseModel> GetPromptResponse(string prompt)
    {
        return null;
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        throw new NotImplementedException();
    }
}