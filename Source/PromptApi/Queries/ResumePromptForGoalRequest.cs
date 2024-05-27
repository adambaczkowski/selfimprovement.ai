namespace PromptApi.Queries;

public class ResumePromptForGoalQuery
{
    public string UserId { get; init; }
    public Guid GoalId { get; init; }
}