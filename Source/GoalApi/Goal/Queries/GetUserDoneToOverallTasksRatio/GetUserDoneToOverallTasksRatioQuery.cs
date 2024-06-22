using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GoalApi.Goal.Dtos;
using GoalApi.Goal.Queries.GetSingleGoal;
using LS.Common;
using MediatR;

namespace GoalApi.Goal.Queries.GetUserDoneToOverallTasksRatio;

public class GetUserDoneToOverallTasksRatioQuery : IRequest<UserTasksRatioDto>
{
    public string UserId { get; set; }
}

public class GetUserDoneToOverallTasksRatioQueryHandler(IGenericRepository<Models.Goal> goalRepository)
    : IRequestHandler<GetUserDoneToOverallTasksRatioQuery, UserTasksRatioDto>
{
    public async Task<UserTasksRatioDto> Handle(GetUserDoneToOverallTasksRatioQuery request, CancellationToken cancellationToken)
    {
        var userTasksRatio = await goalRepository.GetQuery()
            .Where(x => x.UserId == request.UserId)
            .GroupBy(x => x.UserId)
            .Select(x => new
            {
                DoneTasks = x.SelectMany(y => y.Tasks).Count(z => z.IsCompleted),
                AllTasks = x.SelectMany(y => y.Tasks).Count()
            })
            .SingleAsync(cancellationToken);

        return new UserTasksRatioDto()
        {
            CompletedTasksCount = userTasksRatio.DoneTasks,
            AllTasksCount = userTasksRatio.AllTasks
        };
    }
}