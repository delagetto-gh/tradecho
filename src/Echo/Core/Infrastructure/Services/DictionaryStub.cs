using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Services;

namespace Infrastructure.Services;

internal class DictionaryStub : IDictionaryService
{
    public Task<bool> IsWordDefinedAsync(string text)
    {
        string[] words = DummyData.Words;

        if (words.Any(word => word.Equals(text, StringComparison.OrdinalIgnoreCase)))
            return Task.FromResult(true);
        else
            return Task.FromResult(false);
    }
}