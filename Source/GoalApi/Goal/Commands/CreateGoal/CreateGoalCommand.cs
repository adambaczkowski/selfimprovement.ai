using AutoMapper;
using GoalApi.Enums;
using GoalApi.Goal.Dtos;
using LS.Common;
using LS.Events.GoalApi;
using LS.Messaging.EventBus;
using MediatR;

namespace GoalApi.Goal.Commands.CreateGoal;

public class CreateGoalCommand : IRequest<GoalDto>
{
    public string UserId { get; set; }
    public GoalCategories Category { get; init; }
    public TimeAvailability TimeAvailability { get; init; }
    public int Duration { get; init; }
    public Experience Experience { get; init; }
    public LearningType LearningType { get; init; }
}

public class CreateGoalCommandHandler(
    IGenericRepository<Models.Goal> goalRepository,
    IMapper mapper,
    IEventBus eventBus)
    : IRequestHandler<CreateGoalCommand, GoalDto>
{
    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = mapper.Map<Models.Goal>(request);
        goalRepository.Add(goal);
        await goalRepository.SaveAsync();
        
        await eventBus.PublishAsync(new GoalCreatedEvent
        {
            GoalId = goal.Id,
            UserId = goal.UserId
        });
        
        return mapper.Map<GoalDto>(goal);
    }
}
