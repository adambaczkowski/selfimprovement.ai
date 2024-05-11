using PromptApi.AI;
using PromptApi.Models;

namespace PromptApi.Services;

public interface IPromptBuilderService
{
    Task<string> CreatePrompt(Guid userId, Guid goalId);
}