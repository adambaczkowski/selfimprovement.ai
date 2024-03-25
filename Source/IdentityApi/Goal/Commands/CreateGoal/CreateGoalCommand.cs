using AutoMapper;
using IdentityApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace IdentityApi.Goal.Commands.CreateGoal;

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

    public CreateGoalCommandHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = _mapper.Map<Models.Goal>(request);
        _goalRepository.Update(goal);
        await _goalRepository.SaveAsync();
        
        return _mapper.Map<GoalDto>(goal);
    }
}
