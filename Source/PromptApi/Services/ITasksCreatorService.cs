using LS.Events.GoalApi;
using LS.Events.PromptApi;
using PromptApi.AI;
using PromptApi.Models;

namespace PromptApi.Services;

public interface ITasksCreatorService
{
    public Task<List<GoalTaskResource>> CreateTaskList(GoalCreatedEvent @event, AiModelName aiModelName = AiModelName.Llama2);
}