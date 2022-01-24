using Microsoft.EntityFrameworkCore;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using SimpleChat.Server.Repository.Data;

namespace SimpleChat.Server.Repository.Repositories;

public class RoomMessageRepository: IRoomMessageRepository
{
    private readonly ApplicationDbContext _context;

    public RoomMessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Add(RoomMessage roomMessage)
    {
        var entityEntry = await _context.RoomMessages.AddAsync(roomMessage);
        await _context.SaveChangesAsync();
        return entityEntry.State == EntityState.Added;
    }

    public async Task<IQueryable<RoomMessage>> GetAllFromRoom(Guid roomId, int count)
    {
        return await Task.FromResult(_context.RoomMessages.Take(count).Where(rm => rm.RoomId == roomId));
    }
}
