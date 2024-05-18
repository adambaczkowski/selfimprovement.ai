using LS.Events.GoalApi;
using LS.Events.PromptApi;
using LS.Messaging.EventBus;
using PromptApi.Services;

namespace PromptApi.EventHandlers;

public class GoalCreatedEventHandler(ITasksCreatorService tasksCreatorService, IEventBus eventBus) : IEventHandler<GoalCreatedEvent>
{
    private readonly ITasksCreatorService _tasksCreatorService = tasksCreatorService;
    private readonly IEventBus _eventBus = eventBus;
    
    public async Task Handle(GoalCreatedEvent @event)
    {
        var tasks = await _tasksCreatorService.CreateTaskList(@event);
        
        await _eventBus.PublishAsync(new TasksForGoalCreatedEvent()
        {
            Tasks = tasks
        });
    }
}