using Publisher;
using Publisher.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPublisher(this IServiceCollection services)
    {
        return services
        .AddScoped<IEventPublisherService, EventPublisherService>()
        .AddHostedService<EventPublishingBackgroundService>();
    }
}
