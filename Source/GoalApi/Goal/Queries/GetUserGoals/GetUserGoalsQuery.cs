using System.Data.Entity;
using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.Goal.Queries.GetUserGoals;

public class GetUserGoalsQuery : IRequest<List<GoalDto>>
{
    public Guid UserId { get; init; }
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
        var goals = await _goalRepository.GetQuery()
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<List<GoalDto>>(goals);
    }
}