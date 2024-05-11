using LS.Events.PromptApi;
using PromptApi.Models;

namespace PromptApi.AI.Llama3;

public class Llama3Model : IAiModel
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