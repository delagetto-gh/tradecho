using System.Threading.Tasks;
using Application.Common.Services;
using Infrastructure.Persistence;

namespace Infrastructure.Services;

internal class TranslationServiceStub : ITranslationService
{
    public Task<string[]> GetSupportedLanguagesAsync() => Task.FromResult(DummyData.SupportedLanguages);

    public Task<(string Translation, string Language)> TranslateWordAsync(string word, string targetLanguage)
    {
        var words = DummyData.Words;
        var wordTranslations = DummyData.Translations;

        var translatedWord = wordTranslations[(word, targetLanguage)];

        return Task.FromResult((translatedWord, targetLanguage));
    }
}