using LS.Events.GoalApi;
using LS.Events.PromptApi;
using OpenAI_API.Models;
using PromptApi.AI;
using PromptApi.AI.GPT35;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class TasksCreatorService(IPromptBuilderService promptBuilderService, IAiModelApiClient aiModelApiClient, IConfiguration configuration) : ITasksCreatorService
{
    public async Task<List<GoalTaskResource>> CreateTaskList(GoalCreatedEvent @event, AiModelName aiModelName = AiModelName.Gpt35)
    {
        var model = AiModelFactory.CreateModel(aiModelName, promptBuilderService, aiModelApiClient, configuration);
        var prompt = await model.BuildPrompt(@event.UserId, @event.GoalId);
        var response = await model.GetPromptResponse(prompt);
        var taskList = model.ProcessModelResponse(response);
        
        return taskList;
    }
}