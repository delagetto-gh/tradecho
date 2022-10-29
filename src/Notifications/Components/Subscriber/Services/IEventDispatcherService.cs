using System.Threading.Tasks;
using MediatR;

namespace Subscriber;

internal interface IEventDispatcherService
{
    Task DispatchEventAsync(INotification @event);
}
