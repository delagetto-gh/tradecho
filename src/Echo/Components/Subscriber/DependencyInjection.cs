using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSubscriber(this IServiceCollection services)
    {
        return services;
        // return services
        // .AddHostedService<ConsumerService>();
    }
}
