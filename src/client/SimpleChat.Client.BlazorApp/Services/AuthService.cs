using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Primitives;
using SimpleChat.Client.BlazorApp.Const;
using SimpleChat.Client.BlazorApp.Interfaces;
using SimpleChat.Core.Domain.Models;

namespace SimpleChat.Client.BlazorApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    public async Task<IEnumerable<string>> Register(UserRegister userRegister)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);
        var response = await result.Content.ReadFromJsonAsync<UserLoginResponse>();
        
        if (response is not { Succeeded: true })
            return response != null ? response.Notifications : StringValues.Empty;
        
        SetToken(response.Token);
        return StringValues.Empty;
    }

    public async Task<IEnumerable<string>> Login(UserLogin userLogin)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", userLogin);
        var response = await result.Content.ReadFromJsonAsync<UserLoginResponse>();
        
        if (response is not { Succeeded: true })
            return response != null ? response.Notifications : StringValues.Empty;
        
        SetToken(response.Token);
        return StringValues.Empty;
    }
    
    private async void SetToken(string token)
    {
        await _localStorage.SetItemAsync(AuthConst.AccessToken, token);
        await _authenticationStateProvider.GetAuthenticationStateAsync();
    }
}
