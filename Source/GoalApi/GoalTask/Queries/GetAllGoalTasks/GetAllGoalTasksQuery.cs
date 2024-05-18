using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.GoalTask.Queries.GetAllGoalTasks;

public class GetAllGoalTasksQuery :  IRequest<List<GoalTaskDto>>
{
    public bool? IsCompleted { get; init; }
    public Guid? GoalId { get; init; }
    public DateTime? DayFrom { get; init; }
    public DateTime? DayTo { get; init; }
    public string UserId { get; set; }
}

public class GetAllGoalTasksQueryHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    : IRequestHandler<GetAllGoalTasksQuery, List<GoalTaskDto>>
{
    public async Task<List<GoalTaskDto>> Handle(GetAllGoalTasksQuery request, CancellationToken cancellationToken)
    {
        var goalTasks = await goalTaskRepository.GetQuery()
            .Include(x => x.Goal)
            .Where(x => x.Goal.UserId == request.UserId)
            .WhereIf(request.GoalId.HasValue, x => x.GoalId == request.GoalId)
            .WhereIf(
                request.IsCompleted.HasValue, x => x.IsCompleted == request.IsCompleted)
            .WhereIf(request.DayFrom.HasValue, x => x.Date >= request.DayFrom)
            .WhereIf(request.DayTo.HasValue, x => x.Date <= request.DayTo)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<GoalTaskDto>>(goalTasks);
    }
}