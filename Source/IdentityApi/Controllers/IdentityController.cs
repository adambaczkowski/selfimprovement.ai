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
    public Task<SignInResponse> SignIn([FromBody] SignInCommand command) => mediator.Send(command);

    [Route("SignUp")]
    [HttpPost]
    public Task<SignUpResponse> SignUp([FromBody] SignUpCommand command) => mediator.Send(command);

    [Route("Email/Confirm")]
    [HttpPost]
    public Task ConfirmEmail([FromBody] ConfirmEmailCommand command) => mediator.Send(command);
    
    [Route("Email/ResendConfirmation")]
    [HttpPost]
    public Task ResendConfirmationEmail([FromBody] ResendConfirmationEmailCommand command) => mediator.Send(command);
    
    [Route("Password/RequestReset")]
    [HttpPost]
    public Task RequestPasswordReset([FromBody] RequestPasswordResetCommand command) => mediator.Send(command);
    
    [Route("Password/Reset")]
    [HttpPost]
    public Task ResetPassword([FromBody] ResetPasswordCommand command) => mediator.Send(command);
}