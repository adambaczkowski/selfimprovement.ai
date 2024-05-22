using LS.Events.PromptApi;
using PromptApi.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PromptApi.AI.LLama2;

public class Llama2Model(string name,string apiUrl, IPromptBuilderService promptBuilderService) : IAiModel
{
    public string Name { get; set; } = name;
    public string ApiUrl { get; set; } = apiUrl;

    public async Task<string> BuildPrompt(string userId, Guid goalId)
    {
        return await promptBuilderService.CreatePrompt(userId, goalId);
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        var goalTaskList = JsonSerializer.Deserialize<List<GoalTaskResource>>(responseModel.ResponseJson);

        return goalTaskList;
    }
}