namespace LS.Messaging.EventBus;

public interface IEventBus
{
    Task PublishAsync(Event @event);
}