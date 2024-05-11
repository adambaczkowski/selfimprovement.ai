namespace LS.Messaging.Subscriptions;

public class Subscription
{
    public Type EventType { get; private set; }
    public Type HandlerType { get; private set; }

    public Subscription(Type eventType, Type handlerType)
    {
        EventType = eventType;
        HandlerType = handlerType;
    }
}