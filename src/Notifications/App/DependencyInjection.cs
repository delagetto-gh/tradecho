using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        return services
        .AddApi()
        .AddPublisher()
        .AddSubscriber()
        .AddApplication()
        .AddInfrastructure();
    }

    public static TWebApplication UseNotifications<TWebApplication>(this TWebApplication app) where TWebApplication : IApplicationBuilder, IEndpointRouteBuilder
    {
        return app.UseApi();
    }
}
