using AutoMapper;
using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Dtos;
using GoalApi.GoalTask.Dtos;
using LS.Events.PromptApi;

namespace GoalApi.GoalTask.Mappings;

public class GoalTaskMappingProfile : Profile
{
    public GoalTaskMappingProfile()
    {
        CreateMap<Models.GoalTask, GoalTaskDto>();
        CreateMap<Models.GoalTask, GoalTaskDetailsDto>();
        CreateMap<GoalTaskResource, Models.GoalTask>()
            .ForMember(x => x.EstimatedDuration, src => src.MapFrom(x => TimeSpan.FromMinutes(x.EstimatedDuration)));
    }
}