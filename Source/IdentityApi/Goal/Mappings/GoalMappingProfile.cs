using AutoMapper;
using IdentityApi.Goal.Commands.CreateGoal;
using IdentityApi.Goal.Dtos;

namespace IdentityApi.Goal.Mappings;

public class GoalMappingProfile : Profile
{
    public GoalMappingProfile()
    {
        CreateMap<CreateGoalCommand, Models.Goal>();
        CreateMap<Models.Goal, GoalDto>();
    }
}