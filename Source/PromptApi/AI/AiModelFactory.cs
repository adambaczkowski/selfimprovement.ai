using PromptApi.AI.LLama;

namespace PromptApi.AI;

public class AiModelFactory
{
    public AiModel CreateModel()
    {
        return new LlamaModel();
    }
}