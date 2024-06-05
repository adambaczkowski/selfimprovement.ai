using System.Text.Json;
using LS.Events.PromptApi;
using OpenAI_API.Models;
using PromptApi.Services;

namespace PromptApi.AI.GPT35;

public class Gpt35Model(string name, string apiUrl, IPromptBuilderService promptBuilderService, IConfiguration configuration) : IAiModel
{
    public string Name { get; init; } = name;
    public string ApiUrl { get; init; } = apiUrl;
    public AiModelName AiModelName { get;} = AiModelName.Gpt35;

    public IAiRequestModel RequestModel { get; set; }

    public async Task<string> BuildPrompt(string userId, Guid goalId)
    {
        return await promptBuilderService.CreatePrompt(userId, goalId);
    }

    public async Task<AiResponseModel> GetPromptResponse(string prompt)
    {
        var apiKey = configuration[$"{AiModelName.ToString()}_ApiKey"];
        var api = new OpenAI_API.OpenAIAPI(apiKey);
        var chat = api.Chat.CreateConversation();
        chat.Model = Model.ChatGPTTurbo;
        chat.AppendUserInput(prompt);
        var response = await chat.GetResponseFromChatbotAsync();
        return new AiResponseModel()
        {
            response = response
        };
    }

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        return JsonSerializer.Deserialize<List<GoalTaskResource>>(responseModel.response) ?? throw new InvalidOperationException();
    }
}