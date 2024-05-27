using IdentityApi.Identity.Commands;
using IdentityApi.Identity.Commands.RequestPasswordReset;
using IdentityApi.Identity.Commands.SendConfirmEmail;
using IdentityApi.User.Commands.CreateUserProfile;
using IdentityApi.User.Dtos;
using IdentityApi.User.Queries;
using LS.Common;
using LS.Startup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers;


[Route("api/User")]
public class UserController(IMediator mediator, ICurrentUserService currentUserService) : Controller
{
    [Route("/Profile")]
    [HttpPost]
    [Authorize]
    public Task<UserProfileDto> CreateUserProfile([FromForm] CreateUserProfileCommand command)
    {
        command.UserId = currentUserService.UserId;
        return mediator.Send(command);
    }

    [Route("/Profile")]
    [HttpPut]
    [Authorize]
    public Task<UserProfileDto> EditUserProfile([FromForm] EditUserProfileCommand command) 
    {
        command.UserId = currentUserService.UserId;
        return mediator.Send(command);
    }
    
    [Route("/Profile")]
    [HttpGet]
    [Authorize]
    public Task<UserProfileDto> UserProfileDetails()
    {
        var userId = currentUserService.UserId;
        return mediator.Send(new GetSingleUserProfileQuery()
        {
            UserId = userId
        });
    }
    
    [Route("{userId}/Profile")]
    [HttpGet]
    public Task<UserProfileDto> UserProfileDetailsById([FromRoute]string userId)
    {
        return mediator.Send(new GetSingleUserProfileQuery()
        {
            UserId = userId
        });
    }
    
    
}