using LS.Events.PromptApi;
using PromptApi.Services;

namespace PromptApi.AI.GPT35;

public class Gpt35Model(string name, string apiUrl, IPromptBuilderService promptBuilderService) : IAiModel
{
    public string Name { get; init; } = name;
    public string ApiUrl { get; init; } = apiUrl;
    public AiModelName AiModelName { get;} = AiModelName.Gpt35;

    public IAiRequestModel RequestModel { get; set; }

    public Task<string> BuildPrompt(string userId, Guid goalId)
    {
        throw new NotImplementedException();
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        throw new NotImplementedException();
    }
}