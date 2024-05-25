using IdentityApi.Identity.Commands;
using IdentityApi.Identity.Commands.RequestPasswordReset;
using IdentityApi.Identity.Commands.SendConfirmEmail;
using IdentityApi.User.Commands.CreateUserProfile;
using IdentityApi.User.Dtos;
using IdentityApi.User.Queries;
using LS.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers;


[Route("api/User")]
public class UserController(IMediator mediator) : Controller
{
    [Route("/Profile")]
    [HttpPost]
    public Task<UserProfileDto> CreateUserProfile([FromBody] CreateUserProfileCommand command) => mediator.Send(command);

    [Route("/Profile")]
    [HttpPut]
    public Task<UserProfileDto> EditUserProfile([FromBody] EditUserProfileCommand command) => mediator.Send(command);
    
    [Route("/{id}/Profile")]
    [HttpGet]
    public Task<UserProfileDto> UserProfileDetails([FromRoute]string id)
    {
        return mediator.Send(new GetSingleUserProfileQuery()
        {
            UserId = id
        });
    }
}