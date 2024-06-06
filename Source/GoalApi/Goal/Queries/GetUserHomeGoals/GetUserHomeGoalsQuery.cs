using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Goal.Queries.GetUserHomeGoals;

public abstract class GetUserHomeGoalsQuery : IRequest<List<GoalHomeDto>>
{
    public string UserId { get; set; }
}

public class GetUserHomeGoalsQueryHandler(IGenericRepository<Models.Goal> goalRepository)
    : IRequestHandler<GetUserHomeGoalsQuery, List<GoalHomeDto>>
{
    public async Task<List<GoalHomeDto>> Handle(GetUserHomeGoalsQuery request, CancellationToken cancellationToken)
    {
        var homeGoals = await goalRepository.GetQuery()
            .Include(x => x.Tasks)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new GoalHomeDto()
            {
                Name = x.Name,
                CompletedTasksCount = x.Tasks.Count(y => y.IsCompleted),
                AllTasksCount = x.Tasks.Count()
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return homeGoals;
    }
}