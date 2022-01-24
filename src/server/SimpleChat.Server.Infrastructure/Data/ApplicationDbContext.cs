using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Repository.Data;

public class ApplicationDbContext: IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomMessage> RoomMessages { get; set; }
}
