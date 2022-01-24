namespace SimpleChat.Core.Domain.Models;

public class RoomCreateResponse
{
    public bool Succeeded { get; set; }
    public string RoomId { get; set; }
    public IEnumerable<string> Notifications { get; set; }
}
