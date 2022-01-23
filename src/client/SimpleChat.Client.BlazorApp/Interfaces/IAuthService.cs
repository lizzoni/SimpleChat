using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Client.BlazorApp.Interfaces;

public interface IAuthService
{
    Task<IEnumerable<string>> Register(UserRegister userRegister);    
    Task<IEnumerable<string>> Login(UserLogin userLogin);    
}
