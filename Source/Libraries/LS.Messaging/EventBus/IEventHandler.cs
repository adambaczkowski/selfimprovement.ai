namespace LS.Messaging.EventBus;

public interface IEventHandler<in TIntegrationEvent> : IEventHandler
    where TIntegrationEvent : Event
{
    Task Handle(TIntegrationEvent @event);

    Task IEventHandler.Handle(Event @event) => Handle((TIntegrationEvent)@event);
}

public interface IEventHandler
{
    Task Handle(Event @event);
}