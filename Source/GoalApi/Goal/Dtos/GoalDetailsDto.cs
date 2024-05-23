using GoalApi.Enums;
using LS.Common.Enums.Goal;

namespace GoalApi.Goal.Dtos;

public class GoalDetailsDto
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public string Name { get; init; }
    public Goals GoalFriendlyName { get; init; }
    public GoalCategories Category { get; init; }
    public UserAdvancement UserAdvancement { get; init; }
    public TimeAvailabilityPerDay TimeAvailabilityPerDay { get; init; }
    public TimeAvailabilityPerWeek TimeAvailabilityPerWeek { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Experience Experience { get; init; }
    
    public LearningType LearningType { get; init; }
    public string UserInput { get; init; }
}