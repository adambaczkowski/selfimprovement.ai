using IdentityApi.Exceptions;
using IdentityApi.Extensions;
using IdentityApi.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Identity.Commands;

public class ResetPasswordCommand : IRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string Code { get; init; }
}

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<Models.User> _userManager;

    public ResetPasswordCommandHandler(UserManager<Models.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ApiException("User not found", "404");
        }

        var token = WebExtensions.WebDecodeCode(request.Code);
        var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            
        if (!result.Succeeded)
        {
            throw new ApiException(string.Join(Environment.NewLine, result.Errors.Select(err => $"{err.Code}: {err.Description}")), "500");
        }
        
        return Unit.Value;
    }
}