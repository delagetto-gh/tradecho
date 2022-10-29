using System.Threading.Tasks;

namespace Publisher.Services;

internal interface IEventPublisherService
{
    Task PublishEventsAsync();
}