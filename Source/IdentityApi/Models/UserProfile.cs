using IdentityApi.User.Enums;
using LS.Common;

namespace IdentityApi.Models;

public class UserProfile : IEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    
    public byte[] ProfileImage { get; set; }
    public string Name { get; init; }
    public string Surname { get; init; }
    // kg
    public int? Weight { get; init; }
    // cm
    public int? Height { get; init; }
    public int? Age { get; init; }
    public Education? EducationLevel { get; init; }
}