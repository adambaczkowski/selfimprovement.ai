using LS.Common;
using MediatR;

namespace GoalApi.GoalTask.Commads.DeleteGoalTask;

public class DeleteGoalTaskCommand : IRequest
{
    public Guid Id { get; init; }
}

public class DeleteGoalTaskCommandHandler : IRequestHandler<DeleteGoalTaskCommand>
{
    private readonly IGenericRepository<Models.GoalTask> _goalTaskRepository;

    public DeleteGoalTaskCommandHandler(IGenericRepository<Models.GoalTask> goalTaskRepository)
    {
        _goalTaskRepository = goalTaskRepository;
    }

    public async Task Handle(DeleteGoalTaskCommand request, CancellationToken cancellationToken)
    {
        _goalTaskRepository.Remove(request.Id);
        await _goalTaskRepository.SaveAsync();
    }
}