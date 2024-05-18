using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.GoalTask.Queries.GetSingleGoalTask;

public class GetSingleGoalTaskQuery : IRequest<GoalTaskDetailsDto>
{
    public Guid Id { get; init; }
}

public class GetSingleGoalTaskQueryHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    : IRequestHandler<GetSingleGoalTaskQuery, GoalTaskDetailsDto>
{
    public async Task<GoalTaskDetailsDto> Handle(GetSingleGoalTaskQuery request, CancellationToken cancellationToken)
    {
        var goalTask = await goalTaskRepository.GetByIdAsync(request.Id);

        return mapper.Map<GoalTaskDetailsDto>(goalTask);
    }
}
