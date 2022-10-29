using System.Threading.Tasks;
using MediatR;

namespace Application.Common.Services;

public interface INotificationService
{
    Task NotifyAsync(string notificationtype, INotification notification);
}