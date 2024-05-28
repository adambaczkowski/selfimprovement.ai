namespace PromptApi.AI.Prompts;

public class BasicPrompt : IPromptValues
{
    public string Goal { get; init; }
    public string UserAdvancement { get; init; }
    public int ReachGoalInThisManyWeeks { get; init; }
    public int FreeDaysEachWeek { get; init; }
    public int FreeMinutesEachDay { get; init; }
    public string TodaysDate { get; init; }
}