using GoalApi.Enums;
using LS.Common;

namespace GoalApi.Models;

public class Goal : IEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public UserEntity User { get; set; } 
    
    public GoalCategories Category { get; init; }
    public TimeAvailability TimeAvailability { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Experience Experience { get; init; }
    
    public LearningType LearningType { get; init; }
    public ICollection<GoalTask> Tasks { get; init; } = new List<GoalTask>();
}