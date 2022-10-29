using System.Threading.Tasks;
using MediatR;

namespace Application.Common.Services;

public interface IEventPublisherService
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : INotification;
}