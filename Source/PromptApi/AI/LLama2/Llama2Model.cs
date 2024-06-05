using LS.Events.PromptApi;
using PromptApi.ServiceClients;
using PromptApi.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PromptApi.AI.LLama2;

public class Llama2Model(string name,string apiUrl, IPromptBuilderService promptBuilderService, IAiModelApiClient aiModelApiClient) : IAiModel
{
    public string Name { get; init; } = name;
    public string ApiUrl { get; init; } = apiUrl;
    public AiModelName AiModelName { get; } = AiModelName.Llama2;
    public IAiRequestModel RequestModel { get; set; }

    public async Task<string> BuildPrompt(string userId, Guid goalId)
    {
        return await promptBuilderService.CreatePrompt(userId, goalId);
    }

    public async Task<AiResponseModel> GetPromptResponse(string prompt)
    {
        return await aiModelApiClient.GetPromptResponse(this, new { model = Name, prompt = prompt, stream = false });
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        return JsonSerializer.Deserialize<List<GoalTaskResource>>(responseModel.response) ?? throw new InvalidOperationException();
    }
}