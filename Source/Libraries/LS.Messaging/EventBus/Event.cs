namespace LS.Messaging.EventBus;

public abstract class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}