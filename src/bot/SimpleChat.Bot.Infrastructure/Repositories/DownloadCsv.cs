using System.Net;
using System.Net.Security;
using SimpleChat.Bot.Domain.Interfaces;

namespace SimpleChat.Bot.Infrastructure.Repositories;

public class DownloadCsv: IDownloadCsv
{
    public async Task<string[]?> GetContent(string url)
    {
        try
        {
            var client = new WebClient();
            client.DownloadFile(url, "temp.csv");

            var lines = await File.ReadAllLinesAsync("temp.csv");
            return lines;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
