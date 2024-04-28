﻿using AutoMapper;
using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Dtos;
using GoalApi.GoalTask.Dtos;

namespace GoalApi.GoalTask.Mappings;

public class GoalTaskMappingProfile : Profile
{
    public GoalTaskMappingProfile()
    {
        CreateMap<Models.GoalTask, GoalTaskDto>();
        CreateMap<Models.GoalTask, GoalTaskDetailsDto>();
    }
}