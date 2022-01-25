using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NSubstitute;
using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using SimpleChat.Server.Infrastructure.UnitTests.Mocks;
using SimpleChat.Server.Repository.Repositories;
using Xunit;

namespace SimpleChat.Server.Infrastructure.UnitTests.Repositories;

public class AuthRepositoryTests
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IOptions<LoginSettings> _logginSettings;
    private readonly INotificationContext _notification;
    private readonly AuthRepository _authRepository;

    public AuthRepositoryTests()
    {
        _signInManager = new IdentityMock.SignInManager();
        _userManager = new IdentityMock.UserManager();
        _logginSettings = Substitute.For<IOptions<LoginSettings>>();
        _logginSettings.Value.Returns(LoginSettingsMock.Get()); 
        _notification = Substitute.For<INotificationContext>();
        
        _authRepository = new AuthRepository(_signInManager, _userManager, _logginSettings, _notification);
    }
    
    [Fact]
    public async Task Register_ShouldReturnToken_WhenValidUser()
    {
        // Arrange
        var userRegister = new UserRegister
        {
            Name = Guid.NewGuid().ToString(),
            Email = "test@test.com",
            Password = "@Test123",
            ConfirmPassword = "@Test123"
        };

        // Act
        var result = await _authRepository.Register(userRegister);

        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Register_ShouldReturnEmpty_WhenInvalidUser()
    {
        // Arrange
        var userRegister = new UserRegister
        {
            Name = Guid.NewGuid().ToString(),
            Password = "@Test123",
            ConfirmPassword = "@Test123"
        };

        // Act
        var result = await _authRepository.Register(userRegister);

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Login_ShouldReturnToken_WhenValidUser()
    {
        // Arrange
        var userLogin = new UserLogin
        {
            Email = "test@test.com",
            Password = "@Test123",
        };

        // Act
        var result = await _authRepository.Login(userLogin);

        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Login_ShouldReturnEmpty_WhenInvalidUser()
    {
        // Arrange
        var userLogin = new UserLogin
        {
            Password = "@Test123",
        };

        // Act
        var result = await _authRepository.Login(userLogin);

        // Assert
        result.Should().BeEmpty();
    }
}
