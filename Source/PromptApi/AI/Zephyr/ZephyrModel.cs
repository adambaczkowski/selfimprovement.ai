using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.ServiceClients;
using PromptApi.Services;

namespace PromptApi.AI.Zephyr;

public class ZephyrModel(
    IPromptBuilderService promptBuilderService,
    string apiUrl)
    : IAiModel
{
    private readonly IPromptBuilderService _promptBuilderService = promptBuilderService;

    public string ApiUrl { get; set; } = apiUrl;

    public async Task<string> BuildPrompt()
    {
        return await _promptBuilderService.CreatePrompt(new Guid(), new Guid());
    }

    public Task<List<GoalTaskResource>> ProcessModelResponse(Guid goalId)
    {
        throw new NotImplementedException();
    }
}