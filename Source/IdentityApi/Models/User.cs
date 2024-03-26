using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Models;

public class User : IdentityUser
{
    public Guid UserProfileId { get; init; }
    public List<Goal> Goals { get; init; } = new List<Goal>();
}