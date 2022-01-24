namespace SimpleChat.Server.Domain.Models;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
