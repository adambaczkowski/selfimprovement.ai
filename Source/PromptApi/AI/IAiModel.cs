using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.AI;

public interface IAiModel
{
    public string Name { get; init; }
    public string ApiUrl { get; init; }
    public AiModelName AiModelName { get;}

    public Task<string> BuildPrompt(string userId, Guid goalId);
    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel);
}