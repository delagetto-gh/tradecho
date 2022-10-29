using System.Collections.Generic;
using MediatR;

namespace Subscriber.Services;

internal interface IEventConsumerService
{
    IAsyncEnumerable<INotification> GetConsumedEventsStreamAsync();
}