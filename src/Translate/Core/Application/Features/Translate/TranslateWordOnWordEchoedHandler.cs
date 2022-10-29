using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Services;
using Contracts.Events;
using Domain;
using Domain.Word;
using MediatR;

namespace Application.Features.Translate;

public class TranslateWordOnWordEchoedHandler : INotificationHandler<WordEchoed>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITranslationService _translationService;

    public TranslateWordOnWordEchoedHandler(IUnitOfWork unitOfWork,
                                            ITranslationService translationService)
    {
        _unitOfWork = unitOfWork;
        _translationService = translationService;
    }

    public async Task Handle(WordEchoed @event, CancellationToken cancellationToken)
    {
        var translatableLanguages = await _translationService.GetSupportedLanguagesAsync();

        var translationTasks = translatableLanguages
        .Select(lang => _translationService.TranslateWordAsync(@event.Word, targetLanguage: lang))
        .ToList();

        var word = await _unitOfWork.Words.GetAsync(@event.Word);
        if (word == null)
        {
            word = Word.FromText(@event.Word);
            await _unitOfWork.Words.AddAsync(word);
        }

        while (translationTasks.Any())
        {
            var completedTranslationTask = await Task.WhenAny(translationTasks);
            translationTasks.Remove(completedTranslationTask);
            var translationResult = await completedTranslationTask;
            word.AddTranslation(translationResult.Translation, translationResult.Language);
        }

        await _unitOfWork.CommitAsync();
    }
}
