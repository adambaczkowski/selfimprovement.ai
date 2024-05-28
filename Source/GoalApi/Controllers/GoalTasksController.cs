using GoalApi.Goal.Queries.GetUserGoals;
using GoalApi.GoalTask.Commads.CompleteGoalTask;
using GoalApi.GoalTask.Dtos;
using GoalApi.GoalTask.Queries.GetAllGoalTasks;
using GoalApi.GoalTask.Queries.GetGoalsTasksForDay;
using GoalApi.GoalTask.Queries.GetSingleGoalTask;
using LS.Common;
using LS.Startup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoalApi.Controllers;

[Route("api/GoalTasks/")]
public class GoalTasksController(IMediator mediator, ICurrentUserService currentUserService) : Controller
{
    [Route("Tasks/")]
    [HttpPut]
    public Task<GoalTaskDetailsDto> CompleteTask([FromBody] CompleteGoalTaskCommand command) => mediator.Send(command);
    

    [Authorize]
    [Route("Tasks/")]
    [HttpGet]
    public Task<List<GoalTaskDto>> GetUserGoalTasks([FromQuery] GetAllGoalTasksQuery query)
    {
        query.UserId = currentUserService.UserId;
        return mediator.Send(query);
    }
    
    [Authorize]
    [Route("Tasks/Calendar")]
    [HttpGet]
    public Task<List<GoalTasksForDayDto>> GetUserGoalTasksForCalendar([FromQuery] GetGoalsTasksForDayQuery query)
    {
        query.UserId = currentUserService.UserId;
        return mediator.Send(query);
    }
    
    [Route("Tasks/{id}/Details")]
    [HttpGet]
    public Task<GoalTaskDetailsDto> GetGoalTaskDetails([FromRoute]Guid id)
    {
        return mediator.Send(new GetSingleGoalTaskQuery()
        {
            Id = id
        });
    }
}