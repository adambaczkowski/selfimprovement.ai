using AutoMapper;
using IdentityApi.Models;
using IdentityApi.User.Commands.CreateUserProfile;
using IdentityApi.User.Dtos;

namespace IdentityApi.User.Mappings;

public class UserProfileMapper : Profile
{
    public UserProfileMapper()
    {
        CreateMap<CreateUserProfileCommand, UserProfile>();
        CreateMap<UserProfile, UserProfileDto>();
    }
}