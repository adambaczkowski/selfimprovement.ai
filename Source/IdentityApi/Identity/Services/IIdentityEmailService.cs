using IdentityApi.Models;

namespace IdentityApi.Identity.Services;

public interface IIdentityEmailService
{
    public Task SendConfirmationEmail(Models.User user, string origin);
    public Task SendPasswordResetEmail(Models.User user, string origin);
}