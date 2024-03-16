using IdentityApi.Extensions;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace IdentityApi.Identity.Services;

public class IdentityEmailService : IIdentityEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly UserManager<User> _userManager;

    public IdentityEmailService(IEmailSender emailSender, UserManager<User> userManager)
    {
        _emailSender = emailSender;
        _userManager = userManager;
    }
    
    public async Task SendConfirmationEmail(User user, string origin)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebExtensions.WebEncodeCode(code);
        var queryParams = new Dictionary<string, string>()
        {
            {"code", code},
            {"email", user.Email}
        };

        string callbackUrl = QueryHelpers.AddQueryString($"{origin}/account/confirm", queryParams);

        await _emailSender.SendEmailAsync(user.Email, "Account confirmation",
            $"Click this link to confirm account {callbackUrl}");
    }
        
    public async Task SendPasswordResetEmail(User user, string origin)
    {
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebExtensions.WebEncodeCode(code);
        var queryParams = new Dictionary<string, string>()
        {
            {"code", code},
            {"email", user.Email}
        };

        string callbackUrl = QueryHelpers.AddQueryString($"{origin}/account/reset", queryParams);

        await _emailSender.SendEmailAsync( user.Email, "Password reset",
            $"Click this link to reset password {callbackUrl}");
    }
}