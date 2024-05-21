using GoalApi.Enums;
using LS.Common;

namespace GoalApi.Models;

public class Goal : IEntity
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public GoalCategories Category { get; init; }
    public UserAdvancement UserAdvancement { get; init; }
    public TimeAvailabilityPerDay TimeAvailabilityPerDay { get; init; }
    public TimeAvailabilityPerWeek TimeAvailabilityPerWeek { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Experience Experience { get; init; }
    
    public LearningType LearningType { get; init; }
    public string UserInput { get; init; }
    public ICollection<GoalTask> Tasks { get; init; } = new List<GoalTask>();
}