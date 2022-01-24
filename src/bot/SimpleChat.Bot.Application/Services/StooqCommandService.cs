using SimpleChat.Bot.Application.Interfaces;
using SimpleChat.Bot.Domain.Interfaces;

namespace SimpleChat.Bot.Application.Services;

public class StooqCommandService: ICommandService
{
    private readonly IMessageBroker _messageBroker;
    private readonly IStooqService _stooqService;

    public StooqCommandService(IMessageBroker messageBroker, IStooqService stooqService)
    {
        _messageBroker = messageBroker;
        _stooqService = stooqService;
    }

    private async Task<string> Action(string text)
    {
        if (!text.Trim().StartsWith("/stock="))
            return string.Empty;
        var symbol = text.Split("=")[1];
        return await _stooqService.GetQuoteMessage(symbol);
    }

    public async Task AddCommand(string url, string accessToken, string roomId)
    {
        await _messageBroker.AddHook(url, accessToken, roomId, Action);
    }
}
