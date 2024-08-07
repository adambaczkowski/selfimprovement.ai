﻿using IdentityApi.Extensions;
using IdentityApi.Identity.Services;
using IdentityApi.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Identity.Commands;

public class SignUpCommand : IRequest<SignUpResponse>
{
    public string Email { get; init; }
    public string Name { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}

public class SignUpCommandHandler(UserManager<Models.User> userManager, IIdentityEmailService identityEmailService)
    : IRequestHandler<SignUpCommand, SignUpResponse>
{
    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userProfileId = Guid.NewGuid();
        var user = new Models.User()
        {
            UserName = request.Name,
            Email = request.Email,
            UserProfile = new UserProfile()
            {
                Id = userProfileId
            },
            UserProfileId = userProfileId,
        };
        
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new SignUpResponse()
        {
            Message = "Something went wrong while creating user",
            IsSuccess = false
        };
        
        await identityEmailService.SendConfirmationEmail(user, WebExtensions.origin);

        return new SignUpResponse()
        {
            IsSuccess = true
        };
    }
}