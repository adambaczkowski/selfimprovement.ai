using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.Goal.Queries.GetSingleGoal;

public class GetSingleGoalQuery : IRequest<GoalDetailsDto>
{
    public Guid Id { get; init; }
}

public class GetSingleGoalQueryHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper)
    : IRequestHandler<GetSingleGoalQuery, GoalDetailsDto>
{
    public async Task<GoalDetailsDto> Handle(GetSingleGoalQuery request, CancellationToken cancellationToken)
    {
        var goal = await goalRepository.GetByIdAsync(request.Id);

        return mapper.Map<GoalDetailsDto>(goal);
    }
}
