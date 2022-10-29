using System.Threading.Tasks;
using MediatR;

namespace Subscriber;

internal class EventDispatcherService : IEventDispatcherService
{
    private readonly IPublisher _publisher;

    public EventDispatcherService(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task DispatchEventAsync(INotification @event)
    {
        await _publisher.Publish(@event);
    }
}