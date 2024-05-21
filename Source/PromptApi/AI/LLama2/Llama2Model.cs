using LS.Events.PromptApi;
using Newtonsoft.Json;
using PromptApi.Models;
using PromptApi.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PromptApi.AI.LLama;

public class Llama2Model(string name,string apiUrl, IPromptBuilderService promptBuilderService) : IAiModel
{
    public string Name { get; set; } = name;
    public string ApiUrl { get; set; } = apiUrl;

    public async Task<string> BuildPrompt()
    {
        return await promptBuilderService.CreatePrompt(new Guid(), new Guid());
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        var goalTaskList = JsonSerializer.Deserialize<List<GoalTaskResource>>(responseModel.ResponseJson);

        return goalTaskList;
    }
}