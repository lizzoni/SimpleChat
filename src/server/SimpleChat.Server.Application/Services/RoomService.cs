using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Interfaces;

namespace SimpleChat.Server.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomCreateResponse> Create(Guid userId, string roomName)
    {
        var room = await _roomRepository.Get(roomName);
        if (room != null)
            return new RoomCreateResponse { Succeeded = false, Notifications = new[] { "Room already exists" } };

        var roomId = await _roomRepository.Add(userId, roomName);

        return new RoomCreateResponse { Succeeded = true, RoomId = roomId.ToString() };
    }

    public async Task<IEnumerable<RoomResponse>> GetAll()
    {
        var rooms = await _roomRepository.GetAll();
        return rooms.Select(r => new RoomResponse { Id = r.Id, Name = r.Name });
    }
}
