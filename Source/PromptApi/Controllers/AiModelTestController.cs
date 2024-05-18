using LS.Events.GoalApi;
using LS.Events.PromptApi;
using LS.Messaging.EventBus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromptApi.Models;
using PromptApi.Services;

namespace PromptApi.Controllers;


[Route("api/Prompt")]
public class PromptController(IMediator mediator, ITasksCreatorService tasksCreatorService, IEventBus eventBus) : Controller
{
    private readonly ITasksCreatorService _tasksCreatorService = tasksCreatorService;
    private readonly IEventBus _eventBus = eventBus;
    [Route("Test")]
    [HttpPost]
    public async Task<List<GoalTaskResource>> TestPrompt()
    {
        var ev = new GoalCreatedEvent()
        {
            Message = "",
            GoalId = new Guid(),
            UserId = string.Empty,
        };
        var tasks = await _tasksCreatorService.CreateTaskList(ev);

        _eventBus.Publish(new TasksForGoalCreatedEvent()
        {
            Tasks = tasks
        });
        
        return [];
    }
}