using AutoMapper;
using GoalApi.GoalTask.Dtos;
using LS.Common;
using MediatR;

namespace GoalApi.GoalTask.Commads.CompleteGoalTask;

public class CompleteGoalTaskCommand : IRequest<GoalTaskDetailsDto>
{
    public Guid Id { get; init; }
}

public class CompleteGoalTaskCommandHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    : IRequestHandler<CompleteGoalTaskCommand, GoalTaskDetailsDto>
{
    public async Task<GoalTaskDetailsDto> Handle(CompleteGoalTaskCommand request, CancellationToken cancellationToken)
    {
        var goalTask = await goalTaskRepository.GetByIdAsync(request.Id);

        goalTask.IsCompleted = true;

        await goalTaskRepository.SaveAsync();

        return mapper.Map<GoalTaskDetailsDto>(goalTask);
    }
}