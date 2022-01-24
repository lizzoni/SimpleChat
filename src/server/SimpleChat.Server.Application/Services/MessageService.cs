using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Application.Services;

public class MessageService: IMessageService
{
    private readonly IRoomMessageRepository _roomMessageRepository;
    private readonly IUserRepository _userRepository;

    public MessageService(IRoomMessageRepository roomMessageRepository, IUserRepository userRepository)
    {
        _roomMessageRepository = roomMessageRepository;
        _userRepository = userRepository;
    }
    
    public async Task<bool> PostMessage(string userId, string roomId, DateTime createdAt, string text)
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
            CreatedAt = createdAt
        };

        return await _roomMessageRepository.Add(message);
    }
    
    public async Task<IEnumerable<MessageResponse>> GetMessages(string roomId)
    {
        var messages = _roomMessageRepository.GetAllFromRoom(Guid.Parse(roomId), 50);
        var result = messages.Select(m => new MessageResponse
        {
            UserName = _userRepository.GetUserName(m.UserId),
            Text = m.Message,
            CreatedAt = m.CreatedAt
        }).OrderBy(x => x.CreatedAt).ToList();
        return await Task.FromResult(result);
    }
}
