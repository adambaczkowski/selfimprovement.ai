using LS.Events.PromptApi;

namespace PromptApi.AI.GPT35;

public class Gpt35Model : IAiModel
{
    public string ApiUrl { get; set; }
    public string Prompt { get; set; }
    public Task<string> BuildPrompt()
    {
        throw new NotImplementedException();
    }

    public Task<List<GoalTaskResource>> ProcessModelResponse(Guid goalId)
    {
        throw new NotImplementedException();
    }
}