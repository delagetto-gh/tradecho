using System.Threading.Tasks;
using Api.Hubs;
using Application.Common.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    internal class NotificationServiceSignalR : INotificationService
    {
        private readonly IHubContext<NotificationsHub> _hub;

        public NotificationServiceSignalR(IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }

        public async Task NotifyAsync(string notificationtype, INotification notification)
        {
            await _hub.Clients.All.SendAsync("Notification", notification);
        }
    }
}