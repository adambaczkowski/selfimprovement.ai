using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.GoalTask.Queries.GetGoalsTasksForDay;

public class GetGoalsTasksForDayQuery : IRequest<List<GoalTasksForDayDto>>
{
    public string UserId { get; set; }
    public Guid? GoalId { get; init; }
}

public class GetGoalsTasksForDayQueryHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    : IRequestHandler<GetGoalsTasksForDayQuery, List<GoalTasksForDayDto>>
{
    public async Task<List<GoalTasksForDayDto>> Handle(GetGoalsTasksForDayQuery request, CancellationToken cancellationToken)
    {
        var goalTasksGrouped = await goalTaskRepository.GetQuery()
            .Include(x => x.Goal)
            .WhereIf(request.GoalId.HasValue, x => x.GoalId == request.GoalId)
            .Where(x => x.Goal.UserId == request.UserId)
            .GroupBy(x => x.Date.DayOfYear)
            .ToListAsync(cancellationToken);
        
            var goalTasksForDayDto = new List<GoalTasksForDayDto>();
            foreach (var group in goalTasksGrouped)
            {
                var date = new DateTime(DateTime.Now.Year, 1, 1).AddDays(group.Key - 1);
                var goalTasksForDay = new List<GoalTaskDto>();
                foreach (var goalTask in group)
                {
                    goalTasksForDay.Add(mapper.Map<GoalTaskDto>(goalTask));
                }
                
                goalTasksForDayDto.Add(new GoalTasksForDayDto()
                {
                    Day = date,
                    GoalTasks = goalTasksForDay
                });
            }

            return goalTasksForDayDto;
    }
}