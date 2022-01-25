using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.Repository.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly LoginSettings _loginSettings;
    private readonly INotificationContext _notification;

    public AuthRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<LoginSettings> loginSettings, INotificationContext notification)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _loginSettings = loginSettings.Value;
        _notification = notification;
    }
    
    public async Task<string> Register(UserRegister userRegister)
    {
        var user = new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, userRegister.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, userRegister.Name));
            return await GenerateJwt(userRegister.Email);
        }

        foreach (var error in result.Errors)
            _notification.AddNotification(error.Description);
      
        return string.Empty;
    }

    public async Task<string> Login(UserLogin userLogin)
    {
        var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

        if (result.Succeeded)
            return await GenerateJwt(userLogin.Email);

        _notification.AddNotification("Incorrect username or password");
        return string.Empty;
    }

    private async Task<string> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        var token = CreateToken(claims);

        return token;
    }

    private string CreateToken(IEnumerable<Claim> claims)
    {
        var key = Encoding.ASCII.GetBytes(_loginSettings.Secret);
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = new JwtSecurityToken(
            issuer: _loginSettings.Issuer,
            audience: _loginSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_loginSettings.Expires),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
        return tokenHandler.WriteToken(jwt);
    }
}
