using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SimpleChat.Bot.Infrastructure.Repositories;
using SimpleChat.Core.Domain.Models;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SimpleChat.Bot.Infrastructure.UnitTests.Repositories;

public class LoginRepositoryTests
{
    private readonly LoginRepository _loginRepository;
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoginRepository> _logger;

    public LoginRepositoryTests()
    {
        _httpClient = Substitute.For<HttpClient>();
        _logger = Substitute.For<ILogger<LoginRepository>>();
        
        _loginRepository = new LoginRepository(_httpClient, _logger);
    }
    
    [Fact]
    public async Task GetToken_ShouldReturnEmpty_WhenInvalidLogin()
    {
        // Arrange
        const string url = "http://localhost:5000/api/login";
        const string email = "test@test.com";
        const string password = "@Test123";

        // Act
        var result = await _loginRepository.GetToken(url, email, password);

        // Assert
        result.Should().BeEmpty();
    }
}
