using LS.Events.GoalApi;
using LS.Events.PromptApi;
using LS.Messaging.EventBus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromptApi.Models;
using PromptApi.Queries;
using PromptApi.Services;

namespace PromptApi.Controllers;


[Route("api/Prompt")]
public class PromptController(IMediator mediator, ITasksCreatorService tasksCreatorService, IEventBus eventBus) : Controller
{
    private readonly ITasksCreatorService _tasksCreatorService = tasksCreatorService;
    private readonly IEventBus _eventBus = eventBus;
    // [Route("Test/{goalId}/{userId}")]
    // [HttpPost]
    // public async Task<List<GoalTaskResource>> TestPrompt([FromRoute]Guid goalId, [FromRoute]string userId)
    // {
    //     var ev = new GoalCreatedEvent()
    //     {
    //         Message = "",
    //         GoalId = goalId,
    //         UserId = userId,
    //     };
    //     var tasks = await _tasksCreatorService.CreateTaskList(ev);
    //
    //     _eventBus.Publish(new TasksForGoalCreatedEvent()
    //     {
    //         Tasks = tasks
    //     });
    //     
    //     return [];
    // }

    [Route("Resume")]
    [HttpPost]
    public async Task<string> ResumePromptForGoal([FromBody]ResumePromptForGoalRequest request)
    {
        var ev = new GoalCreatedEvent()
        {
            GoalId = request.GoalId,
            UserId = request.UserId,
        };
        try
        {
            var tasks = await _tasksCreatorService.CreateTaskList(ev);
            _eventBus.Publish(new TasksForGoalCreatedEvent()
            {
                Tasks = tasks
            });
        }
        catch(Exception exception)
        {
            return $"Very bad thing happened: {exception.Message}";
        }
        
        return "I think it went well now wait for the tasks";
    }
}