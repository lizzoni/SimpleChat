using Microsoft.Extensions.Logging;
using SimpleChat.Core.Domain.Extensions;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using SimpleChat.Server.Repository.Data;

namespace SimpleChat.Server.Repository.Repositories;

public class RoomMessageRepository: IRoomMessageRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RoomMessageRepository> _logger;

    public RoomMessageRepository(ApplicationDbContext context, ILogger<RoomMessageRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<bool> Add(RoomMessage roomMessage)
    {
        try
        {
            await _context.RoomMessages.AddAsync(roomMessage);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(() => e.Message);
            return false;
        }
    }

    public IEnumerable<RoomMessage> GetAllFromRoom(Guid roomId, int count)
    {
        return _context.RoomMessages.Where(rm => rm.RoomId == roomId).OrderByDescending(rm => rm.CreatedAt).Take(count);
    }
}
