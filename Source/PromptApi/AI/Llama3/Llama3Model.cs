using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.Services;

namespace PromptApi.AI.Llama3;

public class Llama3Model(string name,string apiUrl, IPromptBuilderService promptBuilderService) : IAiModel
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