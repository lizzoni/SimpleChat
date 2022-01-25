using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using SimpleChat.Server.Infrastructure.UnitTests.Mocks;
using SimpleChat.Server.Repository.Data;
using SimpleChat.Server.Repository.Repositories;
using Xunit;

namespace SimpleChat.Server.Infrastructure.UnitTests.Repositories;

public class UserRepositoryTests
{
    private readonly UserRepository _userRepository;
    private readonly ApplicationDbContext _context;

    public UserRepositoryTests()
    {
        _context = ApplicationDbContextMock.Get();
        _userRepository = new UserRepository(_context);
    }

    [Fact]
    public async Task GetUserName_ShouldReturnUserName_WhenUserIdExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const string name = "Test";
        _context.UserClaims.Add(new IdentityUserClaim<string>
        {
            ClaimType = ClaimTypes.Name,
            ClaimValue = name,
            UserId = userId.ToString()
        });
        await _context.SaveChangesAsync();
        
        // Act
        var result = _userRepository.GetUserName(userId);
        
        // Assert
        result.Should().Be(name);
    }
    
    [Fact]
    public void GetUserName_ShouldReturnEmpty_WhenUserIdDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        
        // Act
        var result = _userRepository.GetUserName(userId);
        
        // Assert
        result.Should().Be(string.Empty);
    }
}
