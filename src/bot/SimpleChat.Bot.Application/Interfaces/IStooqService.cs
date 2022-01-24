namespace SimpleChat.Bot.Application.Interfaces;

public interface IStooqService
{
    Task<string> GetQuoteMessage(string symbol);
}
