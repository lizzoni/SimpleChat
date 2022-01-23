using Microsoft.AspNetCore.Mvc;
using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Interfaces;

namespace SimpleChat.Server.BlazorWebHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: Controller
{
    private readonly INotificationContext _notificationContext;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, INotificationContext notificationContext, IAuthService authService) : base(logger, notificationContext)
    {
        _notificationContext = notificationContext;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var response = await _authService.Register(userRegister);
        
        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var response = await _authService.Login(userLogin);
        
        return Ok(response);
    }
}
