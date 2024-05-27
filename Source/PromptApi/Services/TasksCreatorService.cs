﻿using LS.Events.GoalApi;
using LS.Events.PromptApi;
using PromptApi.AI;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class TasksCreatorService(IPromptBuilderService promptBuilderService, IAiModelApiClient aiModelApiClient, IConfiguration configuration) : ITasksCreatorService
{
    public async Task<List<GoalTaskResource>> CreateTaskList(GoalCreatedEvent @event, AiModelName aiModelName = AiModelName.Llama2)
    {
        var model = AiModelFactory.CreateModel(aiModelName, promptBuilderService);
        var prompt = await model.BuildPrompt(@event.UserId, @event.GoalId);
        var response = await aiModelApiClient.GetPromptResponse(model, new {model = model.Name, prompt = prompt, stream=false});
        var taskList = model.ProcessModelResponse(response);
        
        return taskList;
    }
}