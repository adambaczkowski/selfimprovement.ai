using LS.Events.PromptApi;

namespace PromptApi.AI.GPT35;

public class Gpt35Model(string name, string apiUrl) : IAiModel
{
    public string Name { get; set; } = name;
    public string ApiUrl { get; set; } = apiUrl;

    public Task<string> BuildPrompt()
    {
        throw new NotImplementedException();
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        throw new NotImplementedException();
    }
}