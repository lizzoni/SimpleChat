using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SimpleChat.Server.Repository.Hubs;

[Authorize]
public class ChatHub: Hub
{
    public async Task SendMessage(string room, string user, string message)
    {
        Console.WriteLine($"{room}-{user}: {message}");
        await Clients.All.SendAsync(room, user, message);
    }
}
