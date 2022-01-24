using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleChat.Bot.Domain.Configurations;
using SimpleChat.Bot.Domain.Interfaces;
using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Bot.Infrastructure.Repositories;

public class RoomRepository: IRoomRepository
{
    private readonly LoginSettings _settings;
    private readonly HttpClient _httpClient;

    public RoomRepository(IOptions<LoginSettings> settings, HttpClient httpClient)
    {
        _settings = settings.Value;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RoomResponse>> GetAllRooms()
    {
        try
        {
            var result = await _httpClient.GetAsync(Path.Combine(_settings.Url, "api", "room"));
            return (await result.Content.ReadFromJsonAsync<IEnumerable<RoomResponse>>())!;
        }
        catch (Exception)
        {
            return new List<RoomResponse>();
        }
    }
}
