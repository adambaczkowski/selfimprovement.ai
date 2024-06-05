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
    private const string BasicPromptFileName = "basicPrompt.txt";
    private const string MindPromptFileName = "mindPrompt.txt";
    private const string SportPromptFileName = "sportPrompt.txt";
    private const string CraftPromptFileName = "craftPrompt.txt";
    private const string FormatPromptFileName = "formatPrompt.txt";
    private const string AdditionalInfoPromptFileName = "additionalInfoPrompt.txt";
    public async Task<string> CreatePrompt(string userId, Guid goalId)
    {
        var goal = await goalApiClient.GetSingleGoal(goalId);

        var user = await identityApiClient.GetUserDetails(userId);
        
        var basicPromptValuesObject = new BasicPrompt()
        {
            Goal = goal.GoalFriendlyName.ToFriendlyString(),
            UserAdvancement = goal.UserAdvancement.ToString().ToLower(),
            ReachGoalInThisManyWeeks = CountWeeksBetweenTwoDates(goal.StartDate, goal.EndDate),
            FreeDaysEachWeek = (int)goal.TimeAvailabilityPerWeek,
            FreeMinutesEachDay = (int)goal.TimeAvailabilityPerDay,
            TodaysDate = DateTime.Today.ToShortDateString()
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
        
        var additionalInfoPrompt = String.Empty;
        if (!String.IsNullOrWhiteSpace(goal.UserInput))
        {
            var additionalInfoPromptValuesObject = new AdditionalInfoPrompt()
            {
                Input = goal.UserInput
            };
            additionalInfoPrompt =
                await LoadSinglePrompt(AdditionalInfoPromptFileName, additionalInfoPromptValuesObject);
        }

        var formatPromptValuesObject = new FormatPrompt()
        {
            GoalId = goal.Id,
        };
        var formatPrompt = await LoadSinglePrompt(FormatPromptFileName, formatPromptValuesObject);

        var prompt = ConstructFinalPrompt(basicPrompt: basicPrompt, categoryPrompt: categoryPrompt,
            additionalInfoPrompt: additionalInfoPrompt, formatPrompt: formatPrompt);
        
        return prompt;
    }

    private int CountWeeksBetweenTwoDates(DateTime startDate, DateTime endDate)
    {
        return (int)((endDate - startDate).TotalDays / 7);
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