using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Commands.DeleteGoal;
using GoalApi.Goal.Dtos;
using GoalApi.Goal.Queries.GetSingleGoal;
using GoalApi.Goal.Queries.GetUserDoneToOverallTasksRatio;
using GoalApi.Goal.Queries.GetUserGoals;
using GoalApi.Goal.Queries.GetUserHomeGoals;
using LS.Common;
using LS.Startup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoalApi.Controllers;

[Route("api/Goal")]
public class GoalController(IMediator mediator, ICurrentUserService currentUserService) : Controller
{
    [Authorize]
    [Route("/")]
    [HttpPost]
    public Task<GoalDto> CreateGoal([FromBody] CreateGoalCommand command)
    {
        command.UserId = currentUserService.UserId;
        return mediator.Send(command);
    }
    
    [Route("/")]
    [HttpDelete]
    public Task DeleteGoal([FromBody] DeleteGoalCommand command) => mediator.Send(command);
    

    [Authorize]
    [Route("/UserGoals")]
    [HttpGet]
    public Task<List<GoalDto>> GetUserGoals([FromQuery] GetUserGoalsQuery query)
    {
        query.UserId = currentUserService.UserId;
        
        return mediator.Send(query);
    }
    
    [Authorize]
    [Route("/UserHomeGoals")]
    [HttpGet]
    public Task<List<GoalHomeDto>> GetUserHomeGoals([FromQuery] GetUserHomeGoalsQuery query)
    {
        query.UserId = currentUserService.UserId;
        
        return mediator.Send(query);
    }
    
    [Authorize]
    [Route("/DoneToOverallTasksRatio")]
    [HttpGet]
    public Task<UserTasksRatioDto> GetUserDoneTasksRatio([FromQuery] GetUserDoneToOverallTasksRatioQuery query)
    {
        query.UserId = currentUserService.UserId;
        return mediator.Send(query);
    }
    
    [Route("/{id}/Details")]
    [HttpGet]
    public Task<GoalDetailsDto> GetUserGoalDetails([FromRoute]Guid id)
    {
        return mediator.Send(new GetSingleGoalQuery()
        {
            Id = id
        });
    }
}