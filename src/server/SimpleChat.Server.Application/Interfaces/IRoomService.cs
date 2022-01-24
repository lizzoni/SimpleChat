using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Server.Application.Interfaces;

public interface IRoomService
{
    Task<RoomCreateResponse> Create(Guid userId, string roomName);
    Task<IEnumerable<RoomResponse>> GetAll();
}
