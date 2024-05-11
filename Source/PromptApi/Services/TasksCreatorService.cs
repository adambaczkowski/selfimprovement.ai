using LS.Events.GoalApi;
using LS.Events.PromptApi;
using PromptApi.AI;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class TasksCreatorService(IPromptBuilderService promptBuilderService, IAiModelApiClient aiModelApiClient, IConfiguration configuration) : ITasksCreatorService
{
    private readonly IPromptBuilderService _promptBuilderService = promptBuilderService;
    private readonly IAiModelApiClient _aiModelApiClient = aiModelApiClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<GoalTaskResource>> CreateTaskList(GoalCreatedEvent @event, AiModelType aiModelType = AiModelType.Llama2)
    {
        var aiApiUrl = _configuration[aiModelType.ToString()];
        if (aiApiUrl != null)
        {
            var model = AiModelFactory.CreateModel(AiModelType.Llama2, _promptBuilderService, aiApiUrl);
            var prompt = await model.BuildPrompt();
            var response = await _aiModelApiClient.GetPromptResponse(model, new {model = "llama3", prompt = prompt, stream=false});

            var taskList = await model.ProcessModelResponse(@event.GoalId);
            
            return taskList;
        }

        return null;
    }
    
    
}