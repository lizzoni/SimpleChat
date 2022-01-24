using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Bot.Domain.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<RoomResponse>> GetAllRooms();
}
