using LS.Events.PromptApi;
using PromptApi.Models;
using PromptApi.Services;

namespace PromptApi.AI.LLama;

public class Llama2Model(IPromptBuilderService promptBuilderService, string apiUrl) : IAiModel
{
    public string ApiUrl { get; set; } = apiUrl;

    public async Task<string> BuildPrompt()
    {
        return await promptBuilderService.CreatePrompt(new Guid(), new Guid());
    }

    public async Task<List<GoalTaskResource>> ProcessModelResponse(Guid goalId)
    {
        var goalTaskList = new List<GoalTaskResource>();
        var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        var content =
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        for (int i = 1; i < daysInMonth; i++)
        {
            goalTaskList.Add(new GoalTaskResource()
            {
                Id = new Guid(),
                GoalId = goalId,
                EstimatedDuration = TimeSpan.FromMinutes(15),
                IsCompleted = false,
                Content = content,
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i)
            });
        }

        return goalTaskList;
    }
}