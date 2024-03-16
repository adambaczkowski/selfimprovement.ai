using IdentityApi.Exceptions;
using IdentityApi.Extensions;
using IdentityApi.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Identity.Commands;

public class ConfirmEmailCommand : IRequest
{
    public string Email { get; init; }
    public string Code { get; init; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly UserManager<User> _userManager;

    public ConfirmEmailCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        { 
            throw new ApiException("User not found", StatusCodes.Status404NotFound.ToString());
        }
            

        bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if (isEmailConfirmed)
        {
            throw new ApiException("Email is already confirmed", StatusCodes.Status500InternalServerError.ToString());
        }

        var code = WebExtensions.WebDecodeCode(request.Code);
            
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
        { 
           throw new ApiException("Something went wrong", StatusCodes.Status500InternalServerError.ToString());
        }
        
        return Unit.Value;
    }
}