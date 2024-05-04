using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using LS.Events.GoalApi;
using LS.Messaging.EventBus;
using MediatR;

namespace GoalApi.Goal.Commands.CreateGoal;

public class CreateGoalCommand : IRequest<GoalDto>
{
    public Guid UserId { get; init; }
    public string Category { get; init; }
    public string TimeAvailability { get; init; }
    public DateTime Duration { get; init; }
    public string Expirience { get; init; }
    public string LearningType { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IGenericRepository<Models.Goal> _goalRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public CreateGoalCommandHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper, IEventBus eventBus)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = _mapper.Map<Models.Goal>(request);
        _goalRepository.Add(goal);
        await _goalRepository.SaveAsync();
        
        _eventBus.Publish(new GoalCreatedEvent
        {
            GoalId = goal.Id,
            UserId = goal.UserId
        });
        
        return _mapper.Map<GoalDto>(goal);
    }
}
