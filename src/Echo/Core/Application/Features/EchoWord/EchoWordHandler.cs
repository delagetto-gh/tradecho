using System.Threading;
using System.Threading.Tasks;
using Application.Common.Services;
using MediatR;
using Domain;
using Domain.Word;

namespace Application.Features.EchoWord;

public class EchoWordHandler : IRequestHandler<Contracts.Commands.EchoWord>
{
    private readonly IDictionaryService _dictionaryService;
    private readonly IUnitOfWork _uow;

    public EchoWordHandler(IUnitOfWork uow, IDictionaryService dictionaryService)
    {
        _uow = uow;
        _dictionaryService = dictionaryService;
    }

    public async Task<Unit> Handle(Contracts.Commands.EchoWord cmd, CancellationToken cancellationToken)
    {
        var word = await _uow.Words.GetAsync(cmd.Text);

        if (word != null)
        {
            word.Echo();
        }
        else
        {
            word = Word.FromText(cmd.Text);
            
            if (await WordHasMeaningAsync(word))
            {
                await _uow.Words.AddAsync(word);
                word.Echo();
            }
        }

        await _uow.CommitAsync();

        return Unit.Value;
    }

    private Task<bool> WordHasMeaningAsync(Word word)
    {
        return _dictionaryService.IsWordDefinedAsync(word.Text);
    }
}

