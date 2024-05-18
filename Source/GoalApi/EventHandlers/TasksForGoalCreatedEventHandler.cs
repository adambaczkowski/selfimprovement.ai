using AutoMapper;
using LS.Common;
using LS.Events.PromptApi;
using LS.Messaging.EventBus;

namespace GoalApi.EventHandlers;

public class TasksForGoalCreatedEventHandler(IGenericRepository<Models.GoalTask> goalTaskRepository, IMapper mapper)
    : IEventHandler<TasksForGoalCreatedEvent>
{
    private readonly IGenericRepository<Models.GoalTask> _goalTaskRepository = goalTaskRepository;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(TasksForGoalCreatedEvent @event)
    {
        foreach (var goalTask in @event.Tasks)
        {
            var entity = _mapper.Map<Models.GoalTask>(goalTask);
            _goalTaskRepository.Add(entity);
            await _goalTaskRepository.SaveAsync();
        }
    }
}