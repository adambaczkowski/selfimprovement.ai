using System.Text.Json.Serialization;

namespace LS.Messaging.EventBus;

public class Event
{
    public Event()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [JsonInclude]
    public Guid Id { get; set; }

    [JsonInclude]
    public DateTime CreationDate { get; set; }
}