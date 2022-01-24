using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Client.BlazorApp.Interfaces;

public interface IRoomService
{
    event Action OnChangeRooms;
    IEnumerable<RoomResponse> Rooms { get; set; }
    Task<RoomCreateResponse> Create(RoomCreate roomCreate);
    Task<IEnumerable<RoomResponse>> GetAll();
    Task UpdateRooms();
}
