using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.GoalTask.Queries.GetGoalsTasksForDay;

public class GetGoalsTasksForDayQuery : IRequest<List<GoalTasksForDayDto>>
{
    public Guid UserId { get; init; }
    public Guid GoalId { get; init; }
}

public class GetGoalsTasksForDayQueryHandler : IRequestHandler<GetGoalsTasksForDayQuery ,List<GoalTasksForDayDto>>
{
    private readonly IGenericRepository<Models.GoalTask> _goalTaskRepository;
    private readonly IMapper _mapper;

    public GetGoalsTasksForDayQueryHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    {
        _goalTaskRepository = goalTaskRepository;
        _mapper = mapper;
    }

    public async Task<List<GoalTasksForDayDto>> Handle(GetGoalsTasksForDayQuery request, CancellationToken cancellationToken)
    {
        var goalTasksGrouped = await _goalTaskRepository.GetQuery()
            .Include(x => x.Goal)
            .Where(x => x.GoalId == request.GoalId)
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
                    goalTasksForDay.Add(_mapper.Map<GoalTaskDto>(goalTask));
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