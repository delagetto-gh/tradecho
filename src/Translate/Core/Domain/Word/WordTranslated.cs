using Framework.Ddd;

namespace Domain.Word;

public record WordTranslated(string Word, string Translation, string TranslationLanguage) : Event;