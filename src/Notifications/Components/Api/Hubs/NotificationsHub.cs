using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class NotificationsHub : Hub
{
    public static string Route => "/notifications";
}