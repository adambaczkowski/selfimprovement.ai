namespace PromptApi.Queries;

public class ResumePromptForGoalRequest
{
    public string UserId { get; init; }
    public Guid GoalId { get; init; }
}