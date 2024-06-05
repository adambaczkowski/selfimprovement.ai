using PromptApi.AI.GPT35;
using PromptApi.AI.LLama2;
using PromptApi.AI.Llama3;
using PromptApi.AI.Zephyr;
using PromptApi.ServiceClients;
using PromptApi.Services;

namespace PromptApi.AI;

public static class AiModelFactory
{
    private const string Llama2ApiPath = "api/generate";
    private const string Llama3ApiPath = "";
    private const string ZephyrApiPath = "";
    private const string Gpt35ApiPath = "v1/chat/completions";
    public static IAiModel CreateModel(AiModelName name, IPromptBuilderService promptBuilderService)
    {
        switch (name)
        {
            case AiModelName.Gpt35:
                return new Gpt35Model("gpt35", Gpt35ApiPath, promptBuilderService);
            case AiModelName.Llama2:
                return new Llama2Model("llama2", Llama2ApiPath, promptBuilderService);
            case AiModelName.Llama3:
                return new Llama3Model("llama3", Llama3ApiPath, promptBuilderService);
            case AiModelName.Zephyr:
                return new ZephyrModel("zephyr", ZephyrApiPath, promptBuilderService);
            default:
                return null;
        }
    }
}