using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Domain.Interfaces;

public interface INotificationContext
{
    public void AddNotification(string message, string detail = "");
    public IEnumerable<NotificationMessage> Notifications { get; }
    public bool IsValid { get; }    
}
