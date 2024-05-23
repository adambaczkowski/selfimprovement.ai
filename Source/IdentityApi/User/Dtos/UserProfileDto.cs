using LS.Common.Enums.Identity;

namespace IdentityApi.User.Dtos;

public class UserProfileDto
{
    public Sex? Sex { get; init; }
    public int? Weight { get; init; }
    public int? Height { get; init; }
    public int? Age { get; init; }
    public Education? EducationLevel { get; init; }
}