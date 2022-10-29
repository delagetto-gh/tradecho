using Domain.Word;

namespace Domain;

public interface IUnitOfWork : Framework.Ddd.IUnitOfWork
{
    IWordRepository Words { get; }
}