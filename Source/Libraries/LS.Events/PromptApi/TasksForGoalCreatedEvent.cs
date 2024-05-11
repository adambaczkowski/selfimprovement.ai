using LS.Messaging.EventBus;

namespace LS.Events.PromptApi;

public class TasksForGoalCreatedEvent : Event
{
    public List<GoalTaskResource> Tasks { get; init; }
}