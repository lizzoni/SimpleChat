using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using SimpleChat.Server.Repository.Data;

namespace SimpleChat.Server.Repository.Repositories;

[Authorize]
public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid?> Add(Guid userId, string name)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        var entityEntry = await _context.Rooms.AddAsync(room);
        if (entityEntry.State != EntityState.Added)
            return null;
        await _context.SaveChangesAsync();
        return room.Id;
    }

    public async Task<bool> Remove(Guid roomId)
    {
        var room = await Get(roomId);
        if (room == null)
            return false;
        
        var entityEntry = _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return entityEntry.State == EntityState.Added;
    }

    public async Task<Room?> Get(Guid roomId)
    {
        return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
    }

    public async Task<Room?> Get(string name)
    {
        return await _context.Rooms.FirstOrDefaultAsync(x => EF.Functions.Like(x.Name, name));
    }

    public async Task<IQueryable<Room>> GetAll()
    {
        return await Task.FromResult(_context.Rooms);
    }
}
