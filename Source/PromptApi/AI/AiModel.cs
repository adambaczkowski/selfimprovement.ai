namespace PromptApi.AI;

public abstract class AiModel
{
    public string Path { get; set; }
    public object Prompt { get; set; }
}