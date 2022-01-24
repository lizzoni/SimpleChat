namespace SimpleChat.Server.Domain.Models;

public class RoomMessage
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
}
