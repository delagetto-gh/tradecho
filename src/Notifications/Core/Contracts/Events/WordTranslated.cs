using MediatR;

namespace Contracts.Events;

public record WordTranslated(string Word, string Translation, string TranslationLanguage) : INotification;