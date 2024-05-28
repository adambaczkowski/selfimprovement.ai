using LS.Messaging.EventBus;

namespace LS.Events.GoalApi;

public class GoalCreatedEvent : Event
{
    public Guid GoalId { get; init; }
    public string UserId { get; init; }
}