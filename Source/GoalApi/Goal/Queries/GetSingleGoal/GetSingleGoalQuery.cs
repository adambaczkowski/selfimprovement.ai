using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.Goal.Queries.GetSingleGoal;

public class GetSingleGoalQuery : IRequest<GoalDetailsDto>
{
    public Guid Id { get; init; }
}

public class GetSingleGoalQueryHandler : IRequestHandler<GetSingleGoalQuery, GoalDetailsDto>
{
    private readonly IGenericRepository<Models.Goal> _goalRepository;
    private readonly IMapper _mapper;

    public GetSingleGoalQueryHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
    }

    public async Task<GoalDetailsDto> Handle(GetSingleGoalQuery request, CancellationToken cancellationToken)
    {
        var goal = await _goalRepository.GetByIdAsync(request.Id);

        return _mapper.Map<GoalDetailsDto>(goal);
    }
}
