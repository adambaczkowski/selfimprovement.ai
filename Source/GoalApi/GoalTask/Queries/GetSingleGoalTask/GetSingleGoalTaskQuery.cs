using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.GoalTask.Queries.GetSingleGoalTask;

public class GetSingleGoalTaskQuery : IRequest<GoalTaskDetailsDto>
{
    public Guid Id { get; init; }
}

public class GetSingleGoalTaskQueryHandler : IRequestHandler<GetSingleGoalTaskQuery, GoalTaskDetailsDto>
{
    private readonly IGenericRepository<Models.GoalTask> _goalTaskRepository;
    private readonly IMapper _mapper;

    public GetSingleGoalTaskQueryHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    {
        _goalTaskRepository = goalTaskRepository;
        _mapper = mapper;
    }

    public async Task<GoalTaskDetailsDto> Handle(GetSingleGoalTaskQuery request, CancellationToken cancellationToken)
    {
        var goalTask = await _goalTaskRepository.GetByIdAsync(request.Id);

        return _mapper.Map<GoalTaskDetailsDto>(goalTask);
    }
}
