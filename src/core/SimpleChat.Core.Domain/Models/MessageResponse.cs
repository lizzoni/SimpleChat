namespace SimpleChat.Core.Domain.Models;

public class MessageResponse
{
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; }
    public string Text { get; set; }
}
