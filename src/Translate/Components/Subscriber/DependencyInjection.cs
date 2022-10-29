using Subscriber;
using Subscriber.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSubscriber(this IServiceCollection services)
    {
        return services
        .AddScoped<IEventDispatcherService, EventDispatcherService>()
        .AddSingleton<IEventConsumerService, EventConsumerService>()
        .AddHostedService<EventConsumingBackgroundService>();
    }
}
