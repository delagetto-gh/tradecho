using Framework.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOutbox(this IServiceCollection services)
    {
        return services
        .AddScoped<IOutboxServiceFactory, OutboxServiceFactory>();
    }

    public static DbContextOptionsBuilder UseOutbox(this DbContextOptionsBuilder optionsBuilder)
    {
        return optionsBuilder
        .ReplaceService<IModelCustomizer, OutboxModelCustomiser>();
    }
}