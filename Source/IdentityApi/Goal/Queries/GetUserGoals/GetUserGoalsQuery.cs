using AutoMapper;
using IdentityApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace IdentityApi.Goal.Queries.GetUserGoals;

public class GetUserGoalsQuery : IRequest<List<GoalDto>>
{
    
}

public class GetUserGoalsQueryHandler : IRequestHandler<GetUserGoalsQuery, List<GoalDto>>
{
    private readonly IGenericRepository<Models.Goal> _goalRepository;
    private readonly IMapper _mapper;

    public GetUserGoalsQueryHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
    }

    public async Task<List<GoalDto>> Handle(GetUserGoalsQuery request, CancellationToken cancellationToken)
    {
        var goals = await _goalRepository.GetAllAsync();
        return _mapper.Map<List<GoalDto>>(goals);
    }
}