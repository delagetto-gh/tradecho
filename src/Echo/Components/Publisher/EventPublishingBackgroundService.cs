using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher.Services;

namespace Publisher;

internal class EventPublishingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public EventPublishingBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();
            var eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisherService>();
            await eventPublisher.PublishEventsAsync();
        }
    }
}
