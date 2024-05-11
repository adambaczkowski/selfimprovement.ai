using LS.Events.GoalApi;
using LS.Events.PromptApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromptApi.Models;
using PromptApi.Services;

namespace PromptApi.Controllers;


[Route("api/Prompt")]
public class PromptController(IMediator mediator, ITasksCreatorService tasksCreatorService) : Controller
{
    private readonly ITasksCreatorService _tasksCreatorService = tasksCreatorService;
    [Route("Test")]
    [HttpPost]
    public async Task<List<GoalTaskResource>> TestPrompt()
    {
        var ev = new GoalCreatedEvent()
        {
            Message = "",
            GoalId = new Guid(),
            UserId = new Guid(),
        };
        return await _tasksCreatorService.CreateTaskList(ev);
    }
}