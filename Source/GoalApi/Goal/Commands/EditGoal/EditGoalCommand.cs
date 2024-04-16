using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.Goal.Commands.EditGoal;

public class EditGoalCommand : IRequest<GoalDetailsDto>
{
    public Guid Id { get; init; }
}

public class EditGoalCommandHandler : IRequestHandler<EditGoalCommand, GoalDetailsDto>
{
    private readonly IGenericRepository<Models.Goal> _goalRepository;
    private readonly IMapper _mapper;

    public EditGoalCommandHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
    }

    public async Task<GoalDetailsDto> Handle(EditGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _goalRepository.GetByIdAsync(request.Id);
        
        // edit here
        _goalRepository.Update(goal);
        
        await _goalRepository.SaveAsync();
        return _mapper.Map<GoalDetailsDto>(goal);
    }
}