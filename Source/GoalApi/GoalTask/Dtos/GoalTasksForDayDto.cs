namespace GoalApi.GoalTask.Dtos;

public class GoalTasksForDayDto
{
    public DateTime Day { get; set; }
    public List<GoalTaskDto> GoalTasks { get; set; }
}