using GoalApi.Goal.Commands.CreateGoal;
using GoalApi.Goal.Dtos;
using GoalApi.Goal.Queries.GetSingleGoal;
using GoalApi.Goal.Queries.GetUserGoals;
using LS.Common;
using LS.Startup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoalApi.Controllers;

[Route("api/Goal/")]
public class GoalController(IMediator mediator, ICurrentUserService currentUserService) : Controller
{
    [Authorize]
    [Route("/")]
    [HttpPost]
    public async Task<ApiResponse<GoalDto>> CreateGoal([FromBody] CreateGoalCommand command)
    {
        command.UserId = currentUserService.UserId;
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

    [Authorize]
    [Route("/UserGoals")]
    [HttpGet]
    public async Task<ApiResponse<List<GoalDto>>> GetUserGoals([FromQuery] GetUserGoalsQuery query)
    {
        query.UserId = currentUserService.UserId;
        var apiResponse = new ApiResponse<List<GoalDto>>();
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
    
    [Route("/{id}/Details")]
    [HttpGet]
    public async Task<ApiResponse<GoalDetailsDto>> GetUserGoalDetails([FromRoute]Guid id)
    {
        var apiResponse = new ApiResponse<GoalDetailsDto>();
        try
        {
            var response = await mediator.Send(new GetSingleGoalQuery()
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