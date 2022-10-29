using System.Threading.Tasks;

namespace Application.Common.Services;

public interface IDictionaryService
{
    Task<bool> IsWordDefinedAsync(string word);
}