using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Server.Application.Interfaces;

public interface IAuthService
{
    Task<UserLoginResponse> Register(UserRegister userRegister);    
    Task<UserLoginResponse> Login(UserLogin userLogin);    
}
