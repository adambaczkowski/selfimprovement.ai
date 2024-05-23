using LS.Common;
using LS.Common.Enums.Identity;

namespace IdentityApi.Models;

public class UserProfile : IEntity
{
    public Guid Id { get; init; }
    public string? UserId { get; init; }
    public User User { get; init; }
    
    public string? ProfileImageId { get; set; }
    public Sex? Sex { get; init; }
    public int? Weight { get; init; }
    public int? Height { get; init; }
    public int? Age { get; init; }
    public Education? EducationLevel { get; init; }
}