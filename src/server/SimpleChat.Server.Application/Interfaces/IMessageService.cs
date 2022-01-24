using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Server.Application.Interfaces;

public interface IMessageService
{
    Task<bool> PostMessage(string userId, string roomId, DateTime createdAt, string text);
    Task<IEnumerable<MessageResponse>> GetMessages(string roomId);
}
