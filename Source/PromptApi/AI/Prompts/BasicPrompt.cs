namespace PromptApi.AI.Prompts;

public class BasicPrompt : IPromptValues
{
    public string UserAdvancement { get; init; }
    public int ReachGoalInThisManyDays { get; init; }
    public int FreeDaysEachWeek { get; init; }
    public int FreeMinutesEachDay { get; init; }
}