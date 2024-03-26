using AutoMapper;
using IdentityApi.Goal.Dtos;
using LS.Common;
using MediatR;

namespace IdentityApi.Goal.Commands.CreateGoal;

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

    public async Task<Unit> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        _goalRepository.Remove(request.GoalId);
        await _goalRepository.SaveAsync();

        return Unit.Value;
    }
}