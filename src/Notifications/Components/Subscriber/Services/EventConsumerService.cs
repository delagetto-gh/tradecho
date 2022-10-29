using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using Framework.Outbox;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Subscriber.Services;

internal class EventConsumerService : IEventConsumerService
{
    public async IAsyncEnumerable<INotification> GetConsumedEventsStreamAsync()
    {
        Debug.WriteLine($"Invoked call to {nameof(GetConsumedEventsStreamAsync)} on thread: {Thread.CurrentThread.ManagedThreadId}");

        // using Channel's async stream to keep consumer thread alive 
        // & to propogate received events to cnosumeer as they arrive;
        var eventStream = Channel.CreateUnbounded<INotification>();

        //set-up rabbitmq consumer
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var model = connection.CreateModel();

        const string xchange = "translate";
        model.ExchangeDeclare(exchange: xchange, type: ExchangeType.Fanout);

        var q = model.QueueDeclare();
        model.QueueBind(queue: q.QueueName, exchange: xchange, routingKey: "");

        var consumer = new EventingBasicConsumer(model);
        consumer.Received += (_, receivedMessageArgs) =>
        {
            Debug.WriteLine($"Received new RabbitMQ message on thread: {Thread.CurrentThread.ManagedThreadId}");

            var body = receivedMessageArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var outboxMsg = JsonSerializer.Deserialize<OutboxMessage>(message);
            var eventType = Assembly.Load("Contracts").GetTypes().SingleOrDefault(o => o.Name == outboxMsg.Topic);
            var @event = JsonSerializer.Deserialize(outboxMsg.Content, eventType);

            eventStream.Writer.TryWrite(@event as INotification);
        };
        consumer.Shutdown += (_, shutdownArgs) =>
        {
            eventStream.Writer.Complete(); //signal no more messages coming into channel (will exit the awaitiing enumerator)
        };

        model.BasicConsume(queue: q.QueueName,
                             autoAck: true,
                             consumer: consumer);

        await foreach (var @event in eventStream.Reader.ReadAllAsync())
        {
            yield return @event;
        }
    }
}
