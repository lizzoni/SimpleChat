namespace SimpleChat.Bot.Domain.Interfaces;

public interface IMessageBroker
{
    Task AddHook(string url, string accessToken, string roomId, Func<string, Task<string>> action);
}
