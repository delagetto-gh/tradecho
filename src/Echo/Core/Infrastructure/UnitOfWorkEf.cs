using System.Threading.Tasks;
using Domain.Word;
using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

internal class UnitOfWorkEf : Domain.IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWorkEf(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        Words = new WordRepositoryEf(dbContext);
    }

    public IWordRepository Words { get; }

    public Task CommitAsync() => _dbContext.SaveChangesAsync();
    
    public void Dispose() => _dbContext?.Dispose();
}


