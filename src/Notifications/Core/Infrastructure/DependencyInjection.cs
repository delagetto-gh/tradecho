using Application.Common.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationServiceSignalR>();
        return services;
    }
}
