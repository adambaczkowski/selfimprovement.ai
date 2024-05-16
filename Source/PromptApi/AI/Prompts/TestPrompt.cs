using System.Reflection;

namespace PromptApi.AI.Prompts;

public class TestPrompt : IPromptValues
{
    public string Country { get; set; }
    public string Age { get; set; }
}