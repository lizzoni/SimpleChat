namespace SimpleChat.Bot.Domain.Configurations;

public class LoginSettings
{
    public string Url { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] Rooms { get; set; }
}
