using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Models;

public class User : IdentityUser
{
    public Guid UserProfileId { get; init; }
    public UserProfile UserProfile { get; init; }
}