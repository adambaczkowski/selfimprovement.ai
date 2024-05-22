using AutoMapper;
using IdentityApi.Models;
using IdentityApi.User.Dtos;
using LS.Common;
using LS.Common.Enums.Identity;
using LS.Startup;
using MediatR;

namespace IdentityApi.User.Commands.CreateUserProfile;

public class EditUserProfileCommand : IRequest<UserProfileDto>
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

public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand, UserProfileDto>
{
    private readonly IGenericRepository<UserProfile> _userProfileRepository;
    private readonly IMapper _mapper;

    public EditUserProfileCommandHandler(IGenericRepository<UserProfile> userProfileRepository, IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = _mapper.Map<UserProfile>(request);
        // if (request.ProfileImage is not null)
        // {
        //     userProfile.ProfileImage = await request.ProfileImage.GetBytes();
        // }
        _userProfileRepository.Update(userProfile);
        await _userProfileRepository.SaveAsync();
        
        return _mapper.Map<UserProfileDto>(userProfile);
    }
}