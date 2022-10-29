using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Framework.Ddd;
using Framework.Outbox;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal class AddEventsToOutboxInterceptor : SaveChangesInterceptor
{
    private readonly IOutboxServiceFactory _outboxServiceFactory;

    public AddEventsToOutboxInterceptor(IOutboxServiceFactory outboxServiceFactory)
    {
        _outboxServiceFactory = outboxServiceFactory;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var outboxService = _outboxServiceFactory.Create(eventData.Context);

        var aggregateRoots = eventData
        .Context
        .ChangeTracker
        .Entries<IAggregateRoot>()
        .Select(o => o.Entity)
        .ToList();

        var newOutboxMessages = aggregateRoots
        .SelectMany(aggRoot =>
        {
            var events = aggRoot.Events;

            return events;
        })
        .Select(@event => new OutboxMessage
        {
            Id = @event.Id,
            Topic = @event.GetType().Name,
            Content = JsonSerializer.Serialize(@event, @event.GetType()),
            CreatedAt = @event.CreatedAt
        })
        .ToList()
        .AsReadOnly();

        await outboxService.AddMessagesAsync(newOutboxMessages);

        var baseResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

        aggregateRoots.ForEach(o => o.ClearEvents());

        return baseResult;
    }
}
