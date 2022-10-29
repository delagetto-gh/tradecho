using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(pb =>
            {
                pb
                .WithOrigins("null")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });
        services.AddControllers();
        return services;
    }

    public static TWebApplication UseApi<TWebApplication>(this TWebApplication app) where TWebApplication : IApplicationBuilder, IEndpointRouteBuilder
    {
        app.UseCors();
        app.UseRouting();
        app.MapControllers();
        return app;
    }
}
