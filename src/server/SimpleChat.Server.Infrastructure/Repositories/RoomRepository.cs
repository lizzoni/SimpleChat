using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleChat.Core.Domain.Extensions;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using SimpleChat.Server.Repository.Data;

namespace SimpleChat.Server.Repository.Repositories;

[Authorize]
public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RoomRepository> _logger;

    public RoomRepository(ApplicationDbContext context, ILogger<RoomRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Guid?> Add(Guid userId, string roomName)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = roomName,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        try
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(() => e.Message);
            return null;
        }
    }

    public async Task<bool> Remove(Guid roomId)
    {
        var room = await Get(roomId);
        if (room == null)
            return false;

        try
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(() => e.Message);
            return false;
        }
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
