using IdentityApi.Identity.Commands;
using IdentityApi.Identity.Commands.RequestPasswordReset;
using IdentityApi.Identity.Commands.SendConfirmEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers;


[Route("api/Identity")]
public class IdentityController(IMediator mediator) : Controller
{
    [Route("SignIn")]
    [HttpPost]
    public async Task<SignInResponse> SignIn([FromBody] SignInCommand command)
    {
        var response = await mediator.Send(command);
        return response;
    }

    [Route("SignUp")]
    [HttpPost]
    public async Task<SignUpResponse> SignUp([FromBody] SignUpCommand command)
    {
        var response = await mediator.Send(command);

        return response;
    }

    [Route("Email/Confirm")]
    [HttpPost]
    public async Task ConfirmEmail([FromBody] ConfirmEmailCommand command) => await mediator.Send(command);
    
    [Route("Email/ResendConfirmation")]
    [HttpPost]
    public async Task ResendConfirmationEmail([FromBody] ResendConfirmationEmailCommand command) => await mediator.Send(command);
    
    [Route("Password/RequestReset")]
    [HttpPost]
    public async Task RequestPasswordReset([FromBody] RequestPasswordResetCommand command) => await mediator.Send(command);
    
    [Route("Password/Reset")]
    [HttpPost]
    public async Task ResetPassword([FromBody] ResetPasswordCommand command) => await mediator.Send(command);
}