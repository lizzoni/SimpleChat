using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Application.Services;

public class MessageService: IMessageService
{
    private readonly IRoomMessageRepository _roomMessageRepository;

    public MessageService(IRoomMessageRepository roomMessageRepository)
    {
        _roomMessageRepository = roomMessageRepository;
    }
    
    public async Task<bool> PostMessage(string userId, string roomId, string text)
    {
        if (text.Trim().First() == '/')
            return false;
        
        if (!Guid.TryParse(userId, out var userGuid))
            return false;
        if (!Guid.TryParse(roomId, out var roomGuid))
            return false;
        var message = new RoomMessage { 
            Id = Guid.NewGuid(), 
            RoomId = roomGuid,
            UserId = userGuid,
            Message = text,
            CreatedAt = DateTime.UtcNow
        };

        return await _roomMessageRepository.Add(message);
    }
}
