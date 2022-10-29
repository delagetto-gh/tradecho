using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subscriber.Services;

namespace Subscriber;

internal class EventConsumingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventConsumerService _eventConsumerService;

    public EventConsumingBackgroundService(IEventConsumerService eventConsumerService, IServiceProvider serviceProvider)
    {
        _eventConsumerService = eventConsumerService;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var @event in _eventConsumerService.GetConsumedEventsStreamAsync())
        {
            using var scope = _serviceProvider.CreateScope();
            var dispatcher = scope.ServiceProvider.GetRequiredService<IEventDispatcherService>();
            await dispatcher.DispatchEventAsync(@event);
        }
    }
}
