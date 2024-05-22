using System.Reflection;
using System.Text;
using GoalApi.Enums;
using Microsoft.Extensions.Primitives;
using PromptApi.AI;
using PromptApi.AI.Prompts;
using PromptApi.Helpers;
using PromptApi.Models;
using PromptApi.ServiceClients;

namespace PromptApi.Services;

public class PromptBuilderService(IGoalApiClient goalApiClient, IIdentityApiClient identityApiClient, IBlobStorageService blobStorageService) : IPromptBuilderService
{
    private const string BasicPromptFileName = "basicPrompt";
    private const string MindPromptFileName = "mindPrompt";
    private const string SportPromptFileName = "sportPrompt";
    private const string CraftPromptFileName = "craftPrompt";
    private const string FormatPromptFileName = "formatPrompt";
    private const string AdditionalInfoPromptFileName = "additionalInfoPrompt";
    public async Task<string> CreatePrompt(string userId, Guid goalId)
    {
        var goal = await goalApiClient.GetSingleGoal(new GetSingleGoalQuery()
        {
            GoalId = goalId
        });
        
        var user = await identityApiClient.GetUserDetails(new GetUserDetailsQuery()
        {
            UserId = userId
        });

        var basicPromptValuesObject = new BasicPrompt()
        {
            UserAdvancement = goal.UserAdvancement.ToString().ToLower(),
            ReachGoalInThisManyDays = goal.EndDate.Subtract(goal.StartDate).Days,
            FreeDaysEachWeek = (int)goal.TimeAvailabilityPerWeek,
            FreeMinutesEachDay = (int)goal.TimeAvailabilityPerDay,
        };
        
        var basicPrompt = await LoadSinglePrompt(BasicPromptFileName, basicPromptValuesObject);

        var categoryPrompt = String.Empty;
            switch (goal.Category)
            {
                case GoalCategories.Mind:
                    if (user is { Age: not null, EducationLevel: not null })
                    {
                        var mindPromptValuesObject = new MindPrompt()
                        {
                            AgeInYears = user.Age.Value,
                            Education = user.EducationLevel.Value.ToString().ToLower(),
                        };
                        categoryPrompt = await LoadSinglePrompt(MindPromptFileName, mindPromptValuesObject);
                    }

                    break;
                case GoalCategories.Sport:
                    if (user is { Height: not null, Weight: not null, Age: not null, Sex: not null })
                    {
                        var sportPromptValuesObject = new SportPrompt()
                        {
                            AgeInYears = user.Age.Value,
                            HeightInCm = user.Height.Value,
                            WeightInKg = user.Weight.Value,
                            Sex = user.Sex.Value.ToString().ToLower()
                        };
                        categoryPrompt = await LoadSinglePrompt(SportPromptFileName, sportPromptValuesObject);
                    }

                    break;
                case GoalCategories.Craft:
                    var craftPromptValuesObject = new CraftPrompt()
                    {

                    };
                    categoryPrompt = await LoadSinglePrompt(CraftPromptFileName, craftPromptValuesObject);
                    break;
            }

        var additionalInfoPromptValuesObject = new AdditionalInfoPrompt()
        {
            Input = goal.UserInput
        };
        var additionalInfoPrompt = await LoadSinglePrompt(AdditionalInfoPromptFileName, additionalInfoPromptValuesObject);
        
        var formatPrompt = await LoadSinglePrompt(FormatPromptFileName);

        var prompt = ConstructFinalPrompt(basicPrompt: basicPrompt, categoryPrompt: categoryPrompt,
            additionalInfoPrompt: additionalInfoPrompt, formatPrompt: formatPrompt);
        
        return prompt;
    }

    private async Task<string> LoadSinglePrompt(string promptFileName, IPromptValues? promptValuesObject = null)
    {
        var promptTemplateString = await blobStorageService.DownloadPromptFileAsync(promptFileName);

        if (promptValuesObject != null)
        {
            var promptValuesDict = PromptRenderHelper.ConvertPromptValuesToDictonary(promptValuesObject);
            var promptRenderedString = PromptRenderHelper.Render(promptTemplateString, promptValuesDict);
            return promptRenderedString;
        }

        return promptTemplateString;
    }

    private string ConstructFinalPrompt(string basicPrompt, string categoryPrompt, string additionalInfoPrompt, string formatPrompt)
    {
        var sb = new StringBuilder();
        return sb.Append(categoryPrompt)
            .Append(basicPrompt)
            .Append(additionalInfoPrompt)
            .Append(formatPrompt).ToString();
    }
}