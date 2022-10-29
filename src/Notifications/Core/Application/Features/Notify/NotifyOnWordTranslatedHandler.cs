using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Services;
using Contracts.Events;
using MediatR;

namespace Application.Features.Notify;

public class NotifyOnWordTranslatedHandler : INotificationHandler<WordTranslated>
{
    private readonly INotificationService _notificationService;

    public NotifyOnWordTranslatedHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(WordTranslated notification, CancellationToken cancellationToken)
    {
        var camelCaser = JsonNamingPolicy.CamelCase;
        var camelCasedNotificationName = camelCaser.ConvertName(notification.GetType().Name);
        await _notificationService.NotifyAsync(camelCasedNotificationName, notification);
    }
}
