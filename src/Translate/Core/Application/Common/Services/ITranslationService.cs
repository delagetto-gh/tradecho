using System.Threading.Tasks;

namespace Application.Common.Services;

public interface ITranslationService
{
    Task<string[]> GetSupportedLanguagesAsync();

    Task<(string Translation, string Language)> TranslateWordAsync(string word, string targetLanguage);
}