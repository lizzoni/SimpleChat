using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.Core.Domain.Interfaces;
using SimpleChat.Server.Application.Interfaces;

namespace SimpleChat.Server.Repository.Hubs;

[Authorize]
public class MessageBrokerServer: Hub, IMessageBrokerServer
{
    private readonly IMessageService _messageService;

    public MessageBrokerServer(IMessageService messageService)
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
