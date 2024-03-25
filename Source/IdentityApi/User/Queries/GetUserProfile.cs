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

public class GetSingleUserProfileHandler : IRequestHandler<GetSingleUserProfileQuery, UserProfileDto>
{
    private readonly IGenericRepository<UserProfile> _userProfileRepository;
    private readonly IMapper _mapper;

    public GetSingleUserProfileHandler(IGenericRepository<UserProfile> userProfileRepository, IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(GetSingleUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetByIdAsync(request.Id);
        return _mapper.Map<UserProfileDto>(userProfile);
    }
}