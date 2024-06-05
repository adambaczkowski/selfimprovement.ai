namespace PromptApi.AI.GPT35;

public class Gpt35RequestModel : IAiRequestModel
{
    public string model { get; init; }
    public string prompt { get; init; }
    public object response_format { get; init; }
}