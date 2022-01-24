namespace SimpleChat.Core.Domain.Interfaces;

public interface IMessageBrokerClient
{
    void On(Action<DateTime, string, string> action);
    Task SendMessage(string message);
    Task Start();
    Task Stop();
    bool IsConnected();
}
