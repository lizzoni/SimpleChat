using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Server.Domain.Interfaces;

public interface IAuthRepository
{
    Task<string> Register(UserRegister userRegister);    
    Task<string> Login(UserLogin userLogin);    
}
