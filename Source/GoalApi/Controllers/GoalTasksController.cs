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
    public async Task<ApiResponse<GoalTaskDetailsDto>> CompleteTask([FromBody] CompleteGoalTaskCommand command)
    {
        var apiResponse = new ApiResponse<GoalTaskDetailsDto>();
        try
        {
            var response = await mediator.Send(command);
            apiResponse.Data = response;
            return apiResponse;
        }
        catch (Exception ex)
        {
            apiResponse.Success = false;
            apiResponse.ErrorMessage = ex.Message;
        }

        return apiResponse;
    }

    [Authorize]
    [Route("Tasks/")]
    [HttpGet]
    public async Task<ApiResponse<List<GoalTaskDto>>> GetUserGoalTasks([FromQuery] GetAllGoalTasksQuery query)
    {
        query.UserId = currentUserService.UserId;
        var apiResponse = new ApiResponse<List<GoalTaskDto>>();
        try
        {
            var response = await mediator.Send(query);
            apiResponse.Data = response;
            return apiResponse;
        }
        catch (Exception ex)
        {
            apiResponse.Success = false;
            apiResponse.ErrorMessage = ex.Message;
        }

        return apiResponse;
    }
    
    [Authorize]
    [Route("Tasks/Calendar")]
    [HttpGet]
    public async Task<ApiResponse<List<GoalTasksForDayDto>>> GetUserGoalTasksForCalendar([FromQuery] GetGoalsTasksForDayQuery query)
    {
        query.UserId = currentUserService.UserId;
        var apiResponse = new ApiResponse<List<GoalTasksForDayDto>>();
        try
        {
            var response = await mediator.Send(query);
            apiResponse.Data = response;
            return apiResponse;
        }
        catch (Exception ex)
        {
            apiResponse.Success = false;
            apiResponse.ErrorMessage = ex.Message;
        }

        return apiResponse;
    }
    
    [Route("Tasks/{id}/Details")]
    [HttpGet]
    public async Task<ApiResponse<GoalTaskDetailsDto>> GetGoalTaskDetails([FromRoute]Guid id)
    {
        var apiResponse = new ApiResponse<GoalTaskDetailsDto>();
        try
        {
            var response = await mediator.Send(new GetSingleGoalTaskQuery()
            {
                Id = id
            });
            apiResponse.Data = response;
            return apiResponse;
        }
        catch (Exception ex)
        {
            apiResponse.Success = false;
            apiResponse.ErrorMessage = ex.Message;
        }

        return apiResponse;
    }
}