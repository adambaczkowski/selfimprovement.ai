using LS.Events.GoalApi;
using LS.Events.PromptApi;
using PromptApi.AI;
using PromptApi.AI.GPT35;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class TasksCreatorService(IPromptBuilderService promptBuilderService, IAiModelApiClient aiModelApiClient, IConfiguration configuration) : ITasksCreatorService
{
    public async Task<List<GoalTaskResource>> CreateTaskList(GoalCreatedEvent @event, AiModelName aiModelName = AiModelName.Gpt35)
    {
        var model = AiModelFactory.CreateModel(aiModelName, promptBuilderService);
        var prompt = await model.BuildPrompt(@event.UserId, @event.GoalId);
        model.RequestModel = new Gpt35RequestModel()
        {
            model = "gpt-3.5-turbo",
            response_format = new { type = "json_object" },
            prompt = prompt
        };
        var response = await aiModelApiClient.GetPromptResponse(model, model.RequestModel);
        var taskList = model.ProcessModelResponse(response);
        
        return taskList;
    }
}