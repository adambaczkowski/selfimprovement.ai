﻿using LS.Common;

namespace GoalApi.Models;

public class Goal : IEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public UserEntity User { get; set; } 
    
    public string Category { get; init; }
    public string TimeAvailability { get; init; }
    public DateTime Duration { get; init; }
    public string Expirience { get; init; }
    
    public string LearningType { get; init; }
    public List<GoalTask> Tasks { get; init; } = new List<GoalTask>();
}