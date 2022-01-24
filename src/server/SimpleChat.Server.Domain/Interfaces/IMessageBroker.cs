namespace SimpleChat.Server.Domain.Interfaces;

public interface IMessageBroker
{
    Task SendMessage(string roomId, DateTime createdAt, string message);
}
