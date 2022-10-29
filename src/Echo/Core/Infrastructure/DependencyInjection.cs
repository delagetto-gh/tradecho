using Application.Common.Services;
using Domain;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            var connectionString = sp
            .GetRequiredService<IConfiguration>()
            .GetConnectionString("db");

            var outboxEventsinterceptor = sp.GetRequiredService<AddEventsToOutboxInterceptor>();

            opt
            .UseNpgsql(connectionString)
            .UseOutbox()
            .AddInterceptors(outboxEventsinterceptor);
        });
        services.AddOutbox();
        services.AddScoped<AddEventsToOutboxInterceptor>();
        services.AddScoped<IDictionaryService, DictionaryStub>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEf>();
        return services;
    }
}
