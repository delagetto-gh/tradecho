using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEcho(this IServiceCollection services)
    {
        return services
        .AddApi()
        .AddPublisher()
        .AddSubscriber()
        .AddApplication()
        .AddInfrastructure();
    }

    public static TWebApplication UseEcho<TWebApplication>(this TWebApplication app) where TWebApplication : IApplicationBuilder, IEndpointRouteBuilder
    {
        //TODO: for development only - remove for production 
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); 
        db.Database.EnsureCreated();

        return app.UseApi();
    }
}
