namespace SimpleChat.Core.Domain.Interfaces;

public interface IMessageBrokerServer
{
    Task SendMessage(string roomId, DateTime createdAt, string message);
}
