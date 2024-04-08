using AutoMapper;
using LS.Common;
using MediatR;

namespace GoalApi.Goal.Commands.CreateGoal;

public class DeleteGoalCommand : IRequest
{
    public Guid GoalId { get; init; }
}

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand>
{
    private readonly IGenericRepository<Models.Goal> _goalRepository;

    public DeleteGoalCommandHandler(IGenericRepository<Models.Goal> goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        _goalRepository.Remove(request.GoalId);
        await _goalRepository.SaveAsync();
    }
}