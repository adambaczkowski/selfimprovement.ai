using IdentityApi.Goal.Commands.CreateGoal;
using IdentityApi.Goal.Dtos;
using IdentityApi.Identity.Commands;
using IdentityApi.Identity.Commands.RequestPasswordReset;
using IdentityApi.Identity.Commands.SendConfirmEmail;
using IdentityApi.User.Dtos;
using LS.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers;


[Route("api/Goal")]
public class GoalController : Controller
{
    private readonly IMediator _mediator;

    public GoalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("/")]
    [HttpPost]
    public async Task<ApiResponse<GoalDto>> CreateGoal([FromBody] CreateGoalCommand command)
    {
        var apiResponse = new ApiResponse<GoalDto>();
        try
        {
            var response = await _mediator.Send(command);
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
    public async Task<ApiResponse<string>> CreateGoal([FromBody] DeleteGoalCommand command)
    {
        var apiResponse = new ApiResponse<string>();
        try
        {
            await _mediator.Send(command);
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