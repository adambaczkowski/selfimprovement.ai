using System.Reflection;
using PromptApi.AI.Prompts;

namespace PromptApi.Helpers;

public class PromptRenderHelper
{
    public static string Render(string template, Dictionary<string, string> values)
    {
        return values
            .Aggregate(template, (tmp, kvp) => tmp.Replace($"%{kvp.Key}%", kvp.Value));
    }

    public static Dictionary<string, string> ConvertPromptValuesToDictonary(IPromptValues promptValues)
    {
        return promptValues.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(promptValues, null));
    }
}