namespace SimpleChat.Server.Domain.Models;

public class LoginSettings
{
    public string Secret { get; set; }
    public int Expires { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
