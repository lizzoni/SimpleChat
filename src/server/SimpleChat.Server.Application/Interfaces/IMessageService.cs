namespace SimpleChat.Server.Application.Interfaces;

public interface IMessageService
{
    Task<bool> PostMessage(string userId, string roomId, string text);
}
