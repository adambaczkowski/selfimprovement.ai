namespace LS.Messaging.EventBus;

public interface IEventBus
{
    void Publish<TEvent>(TEvent @event)
        where TEvent : Event;

    void Subscribe<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>;

    void Unsubscribe<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>;
}