using LS.Events.PromptApi;
using PromptApi.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PromptApi.AI.LLama2;

public class Llama2Model(string name,string apiUrl, IPromptBuilderService promptBuilderService) : IAiModel
{
    public string Name { get; set; } = name;
    public string ApiUrl { get; set; } = apiUrl;

    public async Task<string> BuildPrompt(string userId, Guid goalId)
    {
        return await promptBuilderService.CreatePrompt(userId, goalId);
    }

      private string jsonString = @"[
    {
      ""Id"": ""a4723590-b5d1-4c3b-8f6c-e07c423b1055"",
      ""GoalId"": ""e8713aef-9395-4ed6-8ca5-7b523a1a629d"",
      ""Title"": ""Monday Morning Warm-Up"",
      ""Content"": ""Start the day with a 5-minute dynamic warm-up consisting of light cardio and mobility exercises. Include arm circles, leg swings, hip openers, and neck stretches."",
      ""EstimatedDuration"": 5,
      ""Date"": ""2024-05-24"",
      ""IsCompleted"": false
    },
    {
      ""Id"": ""38a1e76c-a7b9-414d-83c6-f3c20e2cad24"",
      ""GoalId"": ""e8713aef-9395-4ed6-8ca5-7b523a1a629d"",
      ""Title"": ""Resistance Band Chest Press"",
      ""Content"": ""Perform 3 sets of 10 reps of resistance band chest press using a medium resistance level. Take 30 seconds rest between sets."",
      ""EstimatedDuration"": 25,
      ""Date"": ""2024-05-25"",
      ""IsCompleted"": false
    },
    {
      ""Id"": ""c9c6e8a1-2f7d-4935-84b8-7e61f0b1466a"",
      ""GoalId"": ""e8713aef-9395-4ed6-8ca5-7b523a1a629d"",
      ""Gitle"": ""Bodyweight Squats"",
      ""Content"": ""Perform 3 sets of 10 reps of bodyweight squats. Take 30 seconds rest between sets."",
      ""EstimatedDuration"": 25,
      ""Date"": ""2024-05-26"",
      ""IsCompleted"": false
    },
    {
      ""Id"": ""e8a9e7d6-f57c-4136-8b7d-43a9278e2584"",
      ""GoalId"": ""e8713aef-9395-4ed6-8ca5-7b523a1a629d"",
      ""Title"": ""Plank Hold"",
      ""Content"": ""Hold a plank position for 30 seconds. Rest for 30 seconds before repeating for a total of 3 repetitions."",
      ""EstimatedDuration"": 25,
      ""Date"": ""2024-05-27"",
      ""IsCompleted"": false
    },
    {
      ""Id"": ""d9e56628-f7a5-4c1b-831b-543861619254"",
      ""GoalId"": ""e8713aef-9395-4ed6-8ca5-7b523a1a629d"",
      ""Title"": ""Cool Down Stretch"",
      ""Content"": ""Finish the workout with a 5-minute stretching routine focusing on the major muscle groups. Include hamstring, quadriceps, chest, back, and hip stretches."",
      ""EstimatedDuration"": 5,
      ""Date"": ""2024-05-28"",
      ""IsCompleted"": false
    }
  ]";

    public List<GoalTaskResource> ProcessModelResponse(AiResponseModel responseModel)
    {
        var goalTaskList = JsonSerializer.Deserialize<List<GoalTaskResource>>(responseModel.response);

        return goalTaskList;
    }
}