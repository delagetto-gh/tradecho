using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Consumer.Services;

internal class PublisherService : BackgroundService
{
    public PublisherService()
    {
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new System.NotImplementedException();
    }
}
