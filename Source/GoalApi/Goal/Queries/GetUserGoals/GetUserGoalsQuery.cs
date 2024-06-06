using AutoMapper;
using GoalApi.Goal.Dtos;
using LS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Goal.Queries.GetUserGoals;

public class GetUserGoalsQuery : IRequest<List<GoalDto>>
{
    public string UserId { get; set; }
}

public class GetUserGoalsQueryHandler(IGenericRepository<Models.Goal> goalRepository, IMapper mapper)
    : IRequestHandler<GetUserGoalsQuery, List<GoalDto>>
{
    public async Task<List<GoalDto>> Handle(GetUserGoalsQuery request, CancellationToken cancellationToken)
    {
        var goals = await goalRepository.GetQuery()
            .Include(x => x.Tasks)
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken);
        
        return mapper.Map<List<GoalDto>>(goals);
    }
}