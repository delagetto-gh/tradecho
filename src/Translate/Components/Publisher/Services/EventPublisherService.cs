using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Framework.Outbox;
using Infrastructure.Peristence;
using RabbitMQ.Client;

namespace Publisher.Services;

internal class EventPublisherService : IEventPublisherService
{
    private readonly AppDbContext _appDbContext;
    private readonly IOutboxServiceFactory _outboxServiceFactory;

    public EventPublisherService(AppDbContext appDbContext, IOutboxServiceFactory outboxServiceFactory)
    {
        _appDbContext = appDbContext;
        _outboxServiceFactory = outboxServiceFactory;
        // _messageBroker = messageBroker;
    }

    public async Task PublishEventsAsync()
    {
        ///note - impl. adheres to AT LEAST ONCE delivery, ergo: consumers wil have to support idempotency\
        var outboxService = _outboxServiceFactory.Create(_appDbContext);

        var unpublishedEvents = await outboxService.GetUnprocessedMessagesAsync();

        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        //publish
        const string xchange = "translate";
        channel.ExchangeDeclare(exchange: xchange, type: ExchangeType.Fanout);
        foreach (var unpublishedEvent in unpublishedEvents)
        {
            var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(unpublishedEvent));
            channel.BasicPublish(exchange: xchange,
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: message);
        }

        await outboxService.MarkMessagesAsProcessedAsync(unpublishedEvents);

        await _appDbContext.SaveChangesAsync();
    }
}

