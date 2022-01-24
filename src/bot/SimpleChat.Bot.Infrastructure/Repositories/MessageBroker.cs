using SimpleChat.Bot.Domain.Interfaces;
using SimpleChat.Core.Client;
using SimpleChat.Core.Domain.Interfaces;

namespace SimpleChat.Bot.Infrastructure.Repositories;

public class MessageBroker : IMessageBroker, IDisposable
{
    private readonly IDictionary<string, IMessageBrokerClient> _clients = new Dictionary<string, IMessageBrokerClient>();

    public async Task AddHook(string url, string accessToken, string roomId, Func<string, Task<string>> action)
    {
        var client = new MessageBrokerClient(url, accessToken, roomId);
        client.On(async (_, _, text) =>
        {
            var message = await action(text);
            if (!string.IsNullOrEmpty(message))
                await client.SendMessage(message);
        });
        await client.Start();
        
        _clients[roomId] = client;
    }

    public void Dispose()
    {
        foreach (var (_, value) in _clients)
            value.Stop();
    }
}
