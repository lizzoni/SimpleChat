using System.Net.Http.Json;
using SimpleChat.Client.BlazorApp.Interfaces;
using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Client.BlazorApp.Services;

public class RoomService : IRoomService
{
    private readonly HttpClient _httpClient;

    public RoomService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public event Action? OnChangeRooms;
    public IEnumerable<RoomResponse> Rooms { get; set; } = ArraySegment<RoomResponse>.Empty;

    public async Task<RoomCreateResponse> Create(RoomCreate roomCreate)
    {
        var result = await _httpClient.PostAsJsonAsync("api/room", roomCreate);
        var response = await result.Content.ReadFromJsonAsync<RoomCreateResponse>();
        if (!response.Succeeded)
            return response;
        await UpdateRooms();
        return response;
    }

    public async Task<IEnumerable<RoomResponse>> GetAll()
    {
        var result = await _httpClient.GetAsync("api/room");
        try
        {
            return (await result.Content.ReadFromJsonAsync<IEnumerable<RoomResponse>>())!;
        }
        catch (Exception)
        {
            return new List<RoomResponse>();
        }
    }

    public async Task<IEnumerable<MessageResponse>> GetMessages(string roomId)
    {
        var result = await _httpClient.GetAsync($"api/room/{roomId}");
        try
        {
            return (await result.Content.ReadFromJsonAsync<IEnumerable<MessageResponse>>())!;
        }
        catch (Exception)
        {
            return new List<MessageResponse>();
        }
    }

    public async Task UpdateRooms()
    {
        Rooms = await GetAll();
        OnChangeRooms?.Invoke();
    }
}
