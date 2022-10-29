using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        return services;
    }

    public static TWebApplication UseApi<TWebApplication>(this TWebApplication app) where TWebApplication : IApplicationBuilder, IEndpointRouteBuilder
    {
        return app;
    }
}
