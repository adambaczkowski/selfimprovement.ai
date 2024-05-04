using PromptApi.Models;

namespace PromptApi.Services;

public interface IPromptBuilderService
{
    Task<Prompt> CreatePrompt(Guid userId, Guid goalId);
}