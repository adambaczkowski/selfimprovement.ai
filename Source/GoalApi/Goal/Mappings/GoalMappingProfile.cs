using AutoMapper;
using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Dtos;

namespace GoalApi.Goal.Mappings;

public class GoalMappingProfile : Profile
{
    public GoalMappingProfile()
    {
        CreateMap<CreateGoalCommand, Models.Goal>()
            .ForMember(x => x.StartDate, src => src.MapFrom(x => DateTime.Now))
            .ForMember(x => x.EndDate, src => src.MapFrom(x => DateTime.Now.AddDays(x.Duration)));
        CreateMap<Models.Goal, GoalDto>();
        CreateMap<Models.Goal, GoalDetailsDto>();
    }
}