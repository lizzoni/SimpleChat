using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleChat.Bot.Domain.Configurations;
using SimpleChat.Bot.Domain.Interfaces;
using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Bot.Infrastructure.Repositories;

public class LoginRepository: ILoginRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoginRepository> _logger;
    private readonly LoginSettings _settings;

    public LoginRepository(IOptions<LoginSettings> settings, HttpClient httpClient, ILogger<LoginRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task<string> GetToken()
    {
        _logger.LogInformation("Logging in...");

        var userLogin = new UserLogin
        {
            Email = _settings.Email,
            Password = _settings.Password
        };
        try
        {
            var result = await _httpClient.PostAsJsonAsync(Path.Combine(_settings.Url, "api","auth", "login"), userLogin);
            var response = await result.Content.ReadFromJsonAsync<UserLoginResponse>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token.Replace("\"", ""));
            _logger.LogInformation("Token: " + response.Token);
            return response.Token;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error logging in. Message: {e.Message}");
            return string.Empty;
        }
    }
}
