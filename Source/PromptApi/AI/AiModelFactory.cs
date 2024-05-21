using PromptApi.AI.GPT35;
using PromptApi.AI.LLama;
using PromptApi.AI.Llama3;
using PromptApi.AI.Zephyr;
using PromptApi.ServiceClients;
using PromptApi.Services;

namespace PromptApi.AI;

public static class AiModelFactory
{
    public static IAiModel CreateModel(AiModelType type, IPromptBuilderService promptBuilderService, string apiUrl)
    {
        switch (type)
        {
            case AiModelType.Gpt35:
                return new Gpt35Model("gpt35", apiUrl);
            case AiModelType.Llama2:
                return new Llama2Model("llama2", apiUrl, promptBuilderService);
            case AiModelType.Llama3:
                return new Llama3Model("llama3", apiUrl, promptBuilderService);
            case AiModelType.Zephyr:
                return new ZephyrModel("zephyr", apiUrl, promptBuilderService);
            default:
                return null;
        }
    }
}