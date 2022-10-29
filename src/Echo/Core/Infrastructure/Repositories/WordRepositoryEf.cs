using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Word;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class WordRepositoryEf : IWordRepository
{
    private readonly AppDbContext _context;

    public WordRepositoryEf(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Word entity) => await _context.Words.AddAsync(entity);

    public async Task<Word> GetAsync(string id) => await _context.Words.FindAsync(id);

    public async Task<IReadOnlyCollection<Word>> GetAsync(Expression<Func<Word, bool>> predicate)
    {
        Expression<Func<Word, bool>> defaultGetAllPredicate = (_ => true);

        var list = await _context.Words.Where(predicate ?? defaultGetAllPredicate)
        .AsNoTracking()
        .ToListAsync();

        return list.AsReadOnly();
    }

    public Task RemoveAsync(Word entity) => Task.FromResult(_context.Words.Remove(entity));

    public Task UpdateAsync(Word entity) => Task.FromResult(_context.Words.Update(entity));
}