using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Interfaces;

namespace SimpleChat.Server.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly INotificationContext _notification;

    public AuthService(IAuthRepository authRepository, INotificationContext notification)
    {
        _authRepository = authRepository;
        _notification = notification;
    }
    
    public async Task<UserLoginResponse> Register(UserRegister userRegister)
    {
        var token = await _authRepository.Register(userRegister);

        if (!_notification.IsValid)
        {
            return new UserLoginResponse
            {
                Succeeded = false,
                Notifications = _notification.Notifications.Select(x => x.Message).ToList()
            };
        }
        
        return new UserLoginResponse { Succeeded = true, Token = token };
    }

    public async Task<UserLoginResponse> Login(UserLogin userLogin)
    {
        var token = await _authRepository.Login(userLogin);
        
        if (!_notification.IsValid)
        {
            return new UserLoginResponse
            {
                Succeeded = false,
                Notifications = _notification.Notifications.Select(x => x.Message).ToList()
            };
        }
        
        return new UserLoginResponse { Succeeded = true, Token = token };
    }
}
