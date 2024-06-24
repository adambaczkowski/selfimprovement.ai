namespace GoalApi.Goal.Dtos;

public class GoalHomeDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int CompletedTasksCount { get; init; }
    public int AllTasksCount { get; init; }
}