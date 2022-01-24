namespace SimpleChat.Server.Domain.Interfaces;

public interface IUserRepository
{
    string GetUserName(Guid userId);
}
