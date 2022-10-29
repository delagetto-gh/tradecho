using System.Reflection;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
        .AddMediatR(Assembly.GetExecutingAssembly())
        // .AddTransient(typeof(IPipelineBehavior<,>), typeof(GlobalExceptionHandlingMiddleware<,>));
        ;
    }
}
