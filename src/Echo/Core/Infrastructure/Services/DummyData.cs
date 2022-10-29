using System.Collections.Generic;

namespace Infrastructure;

internal static class DummyData
{
    public static string[] Words => new[] { "Good", "Love", "Mother" };
    public static string[] SupportedLanguages => new[] { "pt", "sv" };
    public static Dictionary<(string, string), string> Translations => new Dictionary<(string, string), string>
    {
        [(Words[0], SupportedLanguages[0])] = "Bom",
        [(Words[0], SupportedLanguages[1])] = "Bra",
        [(Words[1], SupportedLanguages[0])] = "Amor",
        [(Words[1], SupportedLanguages[0])] = "Kärlek",
        [(Words[2], SupportedLanguages[0])] = "Mãe",
        [(Words[2], SupportedLanguages[0])] = "Mor",
    };
}