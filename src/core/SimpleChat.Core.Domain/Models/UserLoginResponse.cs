namespace SimpleChat.Core.Domain.Models;

public class UserLoginResponse
{
    public bool Succeeded { get; set; }
    public string Token { get; set; }
    public IEnumerable<string> Notifications { get; set; }
}
