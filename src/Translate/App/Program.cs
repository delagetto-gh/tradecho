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
        .AddTranslate();

        var host = builder.Build();
        
        host.UseTranslate();

        host.Run();
    }
}

