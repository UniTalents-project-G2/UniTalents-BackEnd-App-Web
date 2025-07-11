using System.Text.RegularExpressions;

namespace UniTalents_BackEnd_AW.Shared.Infrastructure.Interfaces.ASP.Configurations.Extensions;

public static partial class StringExtensions
{
    public static string ToKebabCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return KebabCaseRegex().Replace(input: text, replacement: "-$1")
            .Trim()
            .ToLower(); // string
    }

    [GeneratedRegex(
        pattern: "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", 
        RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}