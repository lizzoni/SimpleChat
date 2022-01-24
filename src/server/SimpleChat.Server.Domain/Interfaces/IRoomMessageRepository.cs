using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Domain.Interfaces;

public interface IRoomMessageRepository
{
    Task<bool> Add(RoomMessage roomMessage);
    Task<IQueryable<RoomMessage>> GetAllFromRoom(Guid roomId, int count);
}
