using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Outbox;

public interface IOutboxService
{
    Task AddMessagesAsync(IReadOnlyCollection<OutboxMessage> messages);

    Task<IReadOnlyCollection<OutboxMessage>> GetUnprocessedMessagesAsync();

    Task MarkMessagesAsProcessedAsync(IReadOnlyCollection<OutboxMessage> messages);
}
