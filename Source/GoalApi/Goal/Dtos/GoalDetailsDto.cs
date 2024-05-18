using GoalApi.Enums;

namespace GoalApi.Goal.Dtos;

public class GoalDetailsDto
{
    public Guid Id { get; init; }
    public GoalCategories Category { get; init; }
    public TimeAvailability TimeAvailability { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Experience Experience { get; init; }
    
    public LearningType LearningType { get; init; }
}