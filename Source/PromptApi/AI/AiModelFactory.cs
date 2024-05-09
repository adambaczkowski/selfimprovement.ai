using PromptApi.AI.GPT35;
using PromptApi.AI.LLama;
using PromptApi.AI.Llama3;
using PromptApi.AI.Zephyr;
using PromptApi.ServiceClients;
using PromptApi.Services;

namespace PromptApi.AI;

public class AiModelFactory
{
    public static IAiModel CreateModel(AiModelType type, IPromptBuilderService promptBuilderService, string apiUrl)
    {
        switch (type)
        {
            case AiModelType.Gpt35:
                return new Gpt35Model();
            case AiModelType.Llama2:
                return new Llama2Model(promptBuilderService, apiUrl);
            case AiModelType.Llama3:
                return new Llama3Model();
            case AiModelType.Zephyr:
                return new ZephyrModel(promptBuilderService, apiUrl);
            default:
                return null;
        }
    }
}