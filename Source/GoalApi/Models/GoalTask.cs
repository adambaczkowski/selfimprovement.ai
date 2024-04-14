using LS.Common;

namespace GoalApi.Models;

public class GoalTask : IEntity
{
    public Guid Id { get; init; }
    public Guid GoalId { get; init; }
    public Goal Goal { get; init; }
    public string Content { get; init; }
    public DateTime EstimatedDuration { get; init; }
    public bool IsCompleted { get; set; }
    public DateTime Date { get; set; }
}