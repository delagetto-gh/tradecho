using System.Threading.Tasks;
using Domain.Word;
using Infrastructure.Peristence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

internal class UnitOfWorkEf : Domain.IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWorkEf(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        Words = new WordRepositoryEf(dbContext);
    }

    public IWordRepository Words { get; }

    public Task CommitAsync() => _dbContext.SaveChangesAsync();

    public void Dispose() => _dbContext?.Dispose();
}