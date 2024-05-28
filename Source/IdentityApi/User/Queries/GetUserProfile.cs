using AutoMapper;
using IdentityApi.Models;
using IdentityApi.User.Dtos;
using LS.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PromptApi.Services;

namespace IdentityApi.User.Queries;

public class GetSingleUserProfileQuery : IRequest<UserProfileDto>
{
    public string UserId { get; init; }
}

public class GetSingleUserProfileHandler(IGenericRepository<UserProfile> userProfileRepository, IMapper mapper, UserManager<Models.User> userManager, IBlobStorageService blobStorageService)
    : IRequestHandler<GetSingleUserProfileQuery, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(GetSingleUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            var userProfile = await userProfileRepository.GetByIdAsync(user.UserProfileId);
            var userProfileDto = mapper.Map<UserProfileDto>(userProfile);
            if (userProfile.ProfileImageId is not null)
            {
                userProfileDto.ProfileImageData = await blobStorageService.GetProfileImage(userProfile.ProfileImageId.Value, cancellationToken);
            }

            return userProfileDto;
        }

        return null;
    }
}