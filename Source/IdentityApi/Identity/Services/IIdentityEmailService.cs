using IdentityApi.Models;

namespace IdentityApi.Identity.Services;

public interface IIdentityEmailService
{
    public Task SendConfirmationEmail(User user, string origin);
    public Task SendPasswordResetEmail(User user, string origin);
}