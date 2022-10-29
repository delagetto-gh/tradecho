using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPublisher(this IServiceCollection services)
    {
        // services
        // .AddHostedService<ConsumerService>();

             // services
        // .AddHostedService<PublisherService>();
        return services;
    }
}
