using LS.Events.GoalApi;
using LS.Messaging.EventBus;
using PromptApi.Services;

namespace PromptApi.EventHandlers;

public class GoalCreatedEventHandler(IPromptBuilderService promptBuilderService) : IEventHandler<GoalCreatedEvent>
{
    public async Task HandleAsync(GoalCreatedEvent @event)
    {
        await promptBuilderService.CreatePrompt(@event.GoalId, @event.UserId);
    }
    
    
}