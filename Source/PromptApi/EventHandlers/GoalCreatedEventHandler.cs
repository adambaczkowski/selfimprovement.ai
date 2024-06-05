using LS.Events.GoalApi;
using LS.Events.PromptApi;
using LS.Messaging.EventBus;
using PromptApi.AI;
using PromptApi.Services;

namespace PromptApi.EventHandlers;

public class GoalCreatedEventHandler(ITasksCreatorService tasksCreatorService, IEventBus eventBus) : IEventHandler<GoalCreatedEvent>
{
    public async Task HandleAsync(GoalCreatedEvent @event)
    {
        var tasks = await tasksCreatorService.CreateTaskList(@event, AiModelName.Gpt35);
        
        eventBus.Publish(new TasksForGoalCreatedEvent()
        {
            Tasks = tasks
        });
    }
}