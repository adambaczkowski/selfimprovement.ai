using AutoMapper;
using IdentityApi.Models;
using IdentityApi.User.Dtos;
using LS.Common;
using LS.Common.Enums.Identity;
using LS.Startup;
using MediatR;
using PromptApi.Services;

namespace IdentityApi.User.Commands.CreateUserProfile;

public class EditUserProfileCommand : IRequest<UserProfileDto>
{
    public string UserId { get; set; }
    public IFormFile? ProfileImage { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public Sex? Sex { get; init; }
    public int? Weight { get; init; }
    public int? Height { get; init; }
    public int? Age { get; init; }
    public Education? EducationLevel { get; init; }
}

public class EditUserProfileCommandHandler(IGenericRepository<UserProfile> userProfileRepository, IMapper mapper, IBlobStorageService blobStorageService)
    : IRequestHandler<EditUserProfileCommand, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = mapper.Map<UserProfile>(request);
        if (request.ProfileImage is not null)
        {
            using var ms = new MemoryStream();
            await request.ProfileImage.CopyToAsync(ms, cancellationToken);
            ms.Seek(0, SeekOrigin.Begin);
            var addedProfileImageId = await blobStorageService.UploadProfileImage(ms, "image/jpeg", cancellationToken);
            if (addedProfileImageId != Guid.Empty)
            {
                userProfile.ProfileImageId = addedProfileImageId;
            }
        }
        userProfileRepository.Update(userProfile);
        await userProfileRepository.SaveAsync();
        var userProfileDto = mapper.Map<UserProfileDto>(userProfile);
        if (userProfile.ProfileImageId is not null)
        {
            userProfileDto.ProfileImageData = await blobStorageService.GetProfileImage(userProfile.ProfileImageId.Value, cancellationToken);
        }

        return userProfileDto;
    }
}