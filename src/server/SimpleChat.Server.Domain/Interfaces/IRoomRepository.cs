using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Domain.Interfaces;

public interface IRoomRepository
{
    Task<Guid?> Add(Guid userId, string name);
    Task<bool> Remove(Guid roomId);
    Task<Room?> Get(Guid roomId);
    Task<Room?> Get(string name);
    Task<IQueryable<Room>> GetAll();
}
