using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.GoalTask.Commads.CompleteGoalTask;

public class CompleteGoalTaskCommand : IRequest<GoalTaskDetailsDto>
{
    public Guid Id { get; init; }
}

public class CompleteGoalTaskCommandHandler : IRequestHandler<CompleteGoalTaskCommand, GoalTaskDetailsDto>
{
    private readonly IGenericRepository<Models.GoalTask> _goalTaskRepository;
    private readonly IMapper _mapper;

    public CompleteGoalTaskCommandHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    {
        _goalTaskRepository = goalTaskRepository;
        _mapper = mapper;
    }

    public async Task<GoalTaskDetailsDto> Handle(CompleteGoalTaskCommand request, CancellationToken cancellationToken)
    {
        var goalTask = await _goalTaskRepository.GetByIdAsync(request.Id);

        goalTask.IsCompleted = true;

        await _goalTaskRepository.SaveAsync();

        return _mapper.Map<GoalTaskDetailsDto>(goalTask);
    }
}