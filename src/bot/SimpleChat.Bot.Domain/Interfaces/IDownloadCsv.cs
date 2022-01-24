namespace SimpleChat.Bot.Domain.Interfaces;

public interface IDownloadCsv
{
    Task<string[]?> GetContent(string url);
}
