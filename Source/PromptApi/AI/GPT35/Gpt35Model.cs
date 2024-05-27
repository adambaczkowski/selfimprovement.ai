using LS.Events.PromptApi;

namespace PromptApi.AI.GPT35;

public class Gpt35Model(string name, string apiUrl) : IAiModel
{
    public string Name { get; init; } = name;
    public string ApiUrl { get; init; } = apiUrl;
    public AiModelName AiModelName { get;} = AiModelName.Gpt35;

    public Task<string> BuildPrompt(string userId, Guid goalId)
    {
        throw new NotImplementedException();
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        throw new NotImplementedException();
    }
}