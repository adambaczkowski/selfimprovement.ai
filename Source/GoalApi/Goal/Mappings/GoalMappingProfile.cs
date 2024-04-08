using AutoMapper;
using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Dtos;

namespace GoalApi.Goal.Mappings;

public class GoalMappingProfile : Profile
{
    public GoalMappingProfile()
    {
        CreateMap<CreateGoalCommand, Models.Goal>();
        CreateMap<Models.Goal, GoalDto>();
    }
}