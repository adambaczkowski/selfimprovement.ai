using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.AI;

public interface IAiModel
{
    public string ApiUrl { get; set; }

    public Task<string> BuildPrompt();
    public Task<List<GoalTaskResource>> ProcessModelResponse(Guid goalId);
}