namespace SimpleChat.Bot.Application.Interfaces;

public interface ICommandService
{
    Task AddCommand(string url, string accessToken, string roomId);
}