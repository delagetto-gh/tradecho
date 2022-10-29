using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Outbox;

internal class OutboxService : IOutboxService
{
    private readonly DbContext _dbContext;

    public OutboxService(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        if (_dbContext.Model.FindEntityType(typeof(OutboxMessage)) is null)
            throw new Exception(@$"No Outbox models were found on {nameof(DbContext)} ({_dbContext.GetType().Name}). 
                                Ensure the required Outbox services have been registered in services.{nameof(DependencyInjection.AddOutbox)} 
                                and dbContextOptionsBuilder.{nameof(DependencyInjection.UseOutbox)}");
    }

    public async Task AddMessagesAsync(IReadOnlyCollection<OutboxMessage> messages)
    {
        await _dbContext
        .Set<OutboxMessage>()
        .AddRangeAsync(messages);
    }

    public async Task<IReadOnlyCollection<OutboxMessage>> GetUnprocessedMessagesAsync()
    {
        var list = await _dbContext
        .Set<OutboxMessage>()
        .AsNoTracking()
        .Where(o => !o.ProcessedAt.HasValue)
        .ToListAsync();

        return list.AsReadOnly();
    }

    public async Task MarkMessagesAsProcessedAsync(IReadOnlyCollection<OutboxMessage> messages)
    {
        var messageIds = messages.Select(o => o.Id).ToHashSet();

        var now = DateTime.UtcNow;

        await _dbContext
        .Set<OutboxMessage>()
        .Where(o => messageIds.Contains(o.Id))
        .ForEachAsync(o => o.ProcessedAt = now);
    }
}
