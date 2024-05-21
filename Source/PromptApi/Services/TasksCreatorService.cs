using LS.Events.GoalApi;
using LS.Events.PromptApi;
using PromptApi.AI;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class TasksCreatorService(IPromptBuilderService promptBuilderService, IAiModelApiClient aiModelApiClient, IConfiguration configuration) : ITasksCreatorService
{
    public async Task<List<GoalTaskResource>> CreateTaskList(GoalCreatedEvent @event, AiModelType aiModelType = AiModelType.Llama2)
    {
        var aiApiUrl = configuration[aiModelType.ToString()];
        if (aiApiUrl != null)
        {
            var model = AiModelFactory.CreateModel(AiModelType.Llama2, promptBuilderService, aiApiUrl);
            var prompt = await model.BuildPrompt();
            var response = await aiModelApiClient.GetPromptResponse(model, new {model = model.Name, prompt = prompt, stream=false, raw = true});
            var taskList = model.ProcessModelResponse(response);
            
            return taskList;
        }

        return null;
    }
}