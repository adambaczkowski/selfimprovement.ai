using IdentityApi.Extensions;
using IdentityApi.Identity.Services;
using IdentityApi.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Identity.Commands;

public class SignUpCommand : IRequest<SignUpResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly UserManager<Models.User> _userManager;
    private readonly IIdentityEmailService _identityEmailService;

    public SignUpCommandHandler(UserManager<Models.User> userManager, IIdentityEmailService identityEmailService)
    {
        _userManager = userManager;
        _identityEmailService = identityEmailService;
    }

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new Models.User()
        {
            UserName = request.Email,
            Email = request.Email,
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new SignUpResponse()
        {
            Message = "Something went wrong while creating user",
            IsSuccess = false
        };
        
        await _identityEmailService.SendConfirmationEmail(user, WebExtensions.origin);

        return new SignUpResponse()
        {
            IsSuccess = true
        };
    }
}