using Microsoft.AspNetCore.Mvc;
using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Interfaces;

namespace SimpleChat.Server.BlazorWebHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController( IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        return await CustomResponse(() => _authService.Register(userRegister));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        return await CustomResponse(() => _authService.Login(userLogin));
    }

    private async Task<IActionResult> CustomResponse(Func<Task<UserLoginResponse>> func)
    {
        if (!ModelState.IsValid)
            return BadRequest(new UserLoginResponse
            {
                Succeeded = false,
                Notifications = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
            });
        
        var response = await func.Invoke();
        if (!response.Succeeded)
            return BadRequest(response);
        
        return Ok(response);
    }
}
