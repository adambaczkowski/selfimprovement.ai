using IdentityApi.Exceptions;
using IdentityApi.Extensions;
using IdentityApi.Identity.Services;
using IdentityApi.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IdentityApi.Identity.Commands.SendConfirmEmail;

public class ResendConfirmationEmailCommand : IRequest
{
    public string Email { get; init; }
}

public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand>
{
    private readonly UserManager<Models.User> _userManager;
    private readonly IIdentityEmailService _identityEmailService;

    public ResendConfirmationEmailCommandHandler(UserManager<Models.User> userManager, IIdentityEmailService identityEmailService)
    {
        _userManager = userManager;
        _identityEmailService = identityEmailService;
    }

    public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        { 
            throw new ApiException("User not found", "404");
        }

        await _identityEmailService.SendConfirmationEmail(user, WebExtensions.origin);
    }
}