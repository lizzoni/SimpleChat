using Flunt.Notifications;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Repository.Notifications;

public class NotificationContext : Notifiable<Notification>, INotificationContext
{
    public new void AddNotification(string message, string detail = "") => base.AddNotification(message, detail);

    public new IEnumerable<NotificationMessage> Notifications => base.Notifications.Select(notification => new NotificationMessage(notification.Key, notification.Message));
}
