namespace SimpleChat.Bot.Domain.Interfaces;

public interface ILoginRepository
{
    Task<string> GetToken(string url, string email, string password);
}
