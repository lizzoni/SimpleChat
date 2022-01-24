using SimpleChat.Core.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace SimpleChat.Core.Client;

public class MessageBrokerClient : IMessageBrokerClient, IDisposable
{
    private readonly string _roomId;
    private readonly HubConnection _hubConnection;

    public MessageBrokerClient(string url, string accessToken, string roomId)
    {
        _roomId = roomId;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{url}chathub", options => { options.AccessTokenProvider = () => Task.FromResult(accessToken.Replace("\"", ""))!; })
            .Build();
    }

    public void On(Action<DateTime, string, string> action)
    {
        _hubConnection.On(_roomId, action);
    }

    public async Task SendMessage(string message)
    {
        await _hubConnection.SendAsync("SendMessage", _roomId, DateTime.UtcNow, message);
    }

    public async Task Start()
    {
        await _hubConnection.StartAsync();
    }

    public async Task Stop()
    {
        if (_hubConnection.State == HubConnectionState.Connected)
            await _hubConnection.StopAsync();
    }

    public bool IsConnected() => _hubConnection?.State == HubConnectionState.Connected;

    public async void Dispose()
    {
        await _hubConnection.DisposeAsync();
    }
}
