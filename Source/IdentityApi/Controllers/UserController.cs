using IdentityApi.Identity.Commands;
using IdentityApi.Identity.Commands.RequestPasswordReset;
using IdentityApi.Identity.Commands.SendConfirmEmail;
using IdentityApi.User.Commands.CreateUserProfile;
using IdentityApi.User.Dtos;
using LS.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers;


[Route("api/User")]
public class UserController(IMediator mediator) : Controller
{
    [Route("Profile")]
    [HttpPost]
    public async Task<ApiResponse<UserProfileDto>> CreateUserProfile([FromBody] CreateUserProfileCommand command)
    {
        var apiResponse = new ApiResponse<UserProfileDto>();
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

    [Route("Profile")]
    [HttpPut]
    public async Task<ApiResponse<UserProfileDto>> EditUserProfile([FromBody] EditUserProfileCommand command)
    {
        var apiResponse = new ApiResponse<UserProfileDto>();
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
}