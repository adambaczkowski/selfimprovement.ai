using AutoMapper;
using IdentityApi.Models;
using IdentityApi.User.Dtos;
using LS.Common;
using MediatR;

namespace IdentityApi.User.Queries;

public class GetSingleUserProfileQuery : IRequest<UserProfileDto>
{
    public Guid Id { get; init; }
}

public class GetSingleUserProfileHandler(IGenericRepository<UserProfile> userProfileRepository, IMapper mapper)
    : IRequestHandler<GetSingleUserProfileQuery, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(GetSingleUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await userProfileRepository.GetByIdAsync(request.Id);
        return mapper.Map<UserProfileDto>(userProfile);
    }
}