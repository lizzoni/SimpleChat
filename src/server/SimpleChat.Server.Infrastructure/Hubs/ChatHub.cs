using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Interfaces;

namespace SimpleChat.Server.Repository.Hubs;

[Authorize]
public class ChatHub: Hub, IMessageBroker
{
    private readonly IMessageService _messageService;

    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    public async Task SendMessage(string roomId, DateTime createdAt, string message)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = Context.User.Identity.Name;

        await _messageService.PostMessage(userId, roomId, createdAt, message);
        
        Console.WriteLine($"[{roomId}] {userName}: {message}");
        await Clients.All.SendAsync(roomId, createdAt, userName, message);
    }
}
