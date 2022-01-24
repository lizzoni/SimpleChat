namespace SimpleChat.Bot.Domain.Interfaces;

public interface ILoginRepository
{
    Task<string> GetToken();
}
