using AutoMapper;
using IdentityApi.Models;
using IdentityApi.User.Dtos;
using IdentityApi.User.Enums;
using LS.Common;
using LS.Startup;
using MediatR;

namespace IdentityApi.User.Commands.CreateUserProfile;

public class CreateUserProfileCommand : IRequest<UserProfileDto>
{
    public Guid UserId { get; init; }
    public IFormFile? ProfileImage { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public int? Weight { get; init; }
    public int? Height { get; init; }
    public int? Age { get; init; }
    public Education? EducationLevel { get; init; }
}

public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, UserProfileDto>
{
    private readonly IGenericRepository<UserProfile> _userProfileRepository;
    private readonly IMapper _mapper;

    public CreateUserProfileCommandHandler(IGenericRepository<UserProfile> userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<UserProfileDto> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = _mapper.Map<UserProfile>(request);
        if (request.ProfileImage is not null)
        {
            userProfile.ProfileImage = await request.ProfileImage.GetBytes();
        }
        _userProfileRepository.Add(userProfile);
        await _userProfileRepository.SaveAsync();
        
        return _mapper.Map<UserProfileDto>(userProfile);
    }
}