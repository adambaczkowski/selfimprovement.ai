using IdentityApi.Exceptions;
using IdentityApi.Extensions;
using IdentityApi.Identity.Services;
using IdentityApi.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace IdentityApi.Identity.Commands.RequestPasswordReset;

public class RequestPasswordResetCommand : IRequest
{
    public string Email { get; init; }
}

public class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdentityEmailService _identityEmailService;

    public RequestPasswordResetCommandHandler(UserManager<User> userManager, IIdentityEmailService identityEmailService)
    {
        _userManager = userManager;
        _identityEmailService = identityEmailService;
    }

    public async Task<Unit> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ApiException("User not found", "404");
        }

        await _identityEmailService.SendPasswordResetEmail(user, WebExtensions.origin);
        return Unit.Value;
    }
}