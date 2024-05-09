namespace LS.Events.PromptApi;

public class GoalTaskResource
{
    public Guid Id { get; init; }
    public Guid GoalId { get; init; }
    public string Content { get; init; }
    public TimeSpan EstimatedDuration { get; init; }
    public bool IsCompleted { get; set; }
    public DateTime Date { get; set; }
}