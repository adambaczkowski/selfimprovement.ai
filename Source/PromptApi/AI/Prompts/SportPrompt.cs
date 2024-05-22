namespace PromptApi.AI.Prompts;

public class SportPrompt : IPromptValues
{
    public int AgeInYears { get; init; }
    public int HeightInCm { get; init; }
    public string Sex { get; init; }
    public int WeightInKg { get; init; }
}