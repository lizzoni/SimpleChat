using SimpleChat.Bot.Application.Interfaces;
using SimpleChat.Bot.Domain.Interfaces;

namespace SimpleChat.Bot.Application.Services;

public class StooqService : IStooqService
{
    private readonly IDownloadCsv _downloadCsv;

    public StooqService(IDownloadCsv downloadCsv)
    {
        _downloadCsv = downloadCsv;
    }
    
    public async Task<string> GetQuoteMessage(string symbol)
    {
        var url = $"https://stooq.com/q/l/?s={symbol}&f=sd2t2ohlcv&h&e=csv";
        var lines = await _downloadCsv.GetContent(url);
        if (lines is not { Length: 2 })
            return $"There is no quote for \"{symbol}\"";
        var fields = lines[1].Split(',');
        var stock = fields[0];
        var quote = fields[6];
        return quote == "N/D" ? $"There is no quote for \"{symbol}\"" : $"{stock} quote is ${quote} per share";
    }
}
