using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Dtos;
using GoalApi.Goal.Queries.GetUserGoals;
using LS.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoalApi.Controllers;


[Route("api/Goal")]
public class GoalController(IMediator mediator) : Controller
{
    [Route("/")]
    [HttpPost]
    public async Task<ApiResponse<GoalDto>> CreateGoal([FromBody] CreateGoalCommand command)
    {
        var apiResponse = new ApiResponse<GoalDto>();
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
    
    [Route("/")]
    [HttpDelete]
    public async Task<ApiResponse<string>> DeleteGoal([FromBody] DeleteGoalCommand command)
    {
        var apiResponse = new ApiResponse<string>();
        try
        {
            await mediator.Send(command);
            return apiResponse;
        }
        catch (Exception ex)
        {
            apiResponse.Success = false;
            apiResponse.ErrorMessage = ex.Message;
        }

        return apiResponse;
    }

    [Route("/")]
    [HttpGet]
    public async Task<ApiResponse<List<GoalDto>>> GetUserGoals([FromQuery] GetUserGoalsQuery query)
    {
        var apiResponse = new ApiResponse<List<GoalDto>>();
        try
        {
            await mediator.Send(query);
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