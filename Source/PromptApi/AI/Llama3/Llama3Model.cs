using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.Services;

namespace PromptApi.AI.Llama3;

public class Llama3Model(string name,string apiUrl, IPromptBuilderService promptBuilderService) : IAiModel
{
    public string Name { get; init; } = name;
    public string ApiUrl { get; init; } = apiUrl;
    public AiModelName AiModelName { get; } = AiModelName.Llama3;

    public Task<string> BuildPrompt(string userId, Guid goalId)
    {
        throw new NotImplementedException();
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        throw new NotImplementedException();
    }
}