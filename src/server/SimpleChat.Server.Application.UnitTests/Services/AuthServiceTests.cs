using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Services;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using Xunit;

namespace SimpleChat.Server.Application.UnitTests.Services;

public class AuthServiceTests
{
    private readonly IAuthRepository _authRepository;
    private readonly INotificationContext _notification;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _authRepository = Substitute.For<IAuthRepository>();
        _notification = Substitute.For<INotificationContext>();
        _authService = new AuthService(_authRepository, _notification);
    }

    [Fact]
    public async Task Register_ShouldReturnSucceeded_WhenSuccessfullyRegistered()
    {
        // Arrange
        var token = Guid.NewGuid().ToString();
        var userRegister = new UserRegister
        {
            Name = Guid.NewGuid().ToString(),
            Email = "test@teste.com",
            Password = "@Test123",
            ConfirmPassword = "@Test123"
        };
        _authRepository.Register(userRegister).Returns(token);
        _notification.IsValid.Returns(true);

        // Act
        var result = await _authService.Register(userRegister);

        // Assert
        result.Succeeded.Should().BeTrue();
        result.Token.Should().Be(token);
    }

    [Fact]
    public async Task Register_ShouldReturnFailed_WhenFailRegister()
    {
        // Arrange
        var userRegister = new UserRegister
        {
            Name = Guid.NewGuid().ToString(),
            Email = "test@teste.com",
            Password = "@Test123",
            ConfirmPassword = "@Test123"
        };
        _notification.Notifications.Returns(new[] { new NotificationMessage("fail", "fail") });

        // Act
        var result = await _authService.Register(userRegister);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.Notifications.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Login_ShouldReturnSucceeded_WhenSuccessfullyLogging()
    {
        // Arrange
        var token = Guid.NewGuid().ToString();
        var userLogin = new UserLogin()
        {
            Email = "test@teste.com",
            Password = "@Test123",
        };
        _authRepository.Login(userLogin).Returns(token);
        _notification.IsValid.Returns(true);

        // Act
        var result = await _authService.Login(userLogin);

        // Assert
        result.Succeeded.Should().BeTrue();
        result.Token.Should().Be(token);
    }

    [Fact]
    public async Task Login_ShouldReturnFailed_WhenFailLogging()
    {
        // Arrange
        var userLogin = new UserLogin()
        {
            Email = "test@teste.com",
            Password = "@Test123",
        };
        _notification.Notifications.Returns(new[] { new NotificationMessage("fail", "fail") });

        // Act
        var result = await _authService.Login(userLogin);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.Notifications.Should().HaveCount(1);
    }
}
