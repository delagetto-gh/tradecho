using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Host;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
        .Services
        .AddEcho();

        var host = builder.Build();
        
        host.UseEcho();

        host.Run();
    }
}

