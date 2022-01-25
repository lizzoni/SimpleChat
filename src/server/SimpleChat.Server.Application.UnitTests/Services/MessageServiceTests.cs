using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using SimpleChat.Server.Application.Services;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using Xunit;

namespace SimpleChat.Server.Application.UnitTests.Services;

public class MessageServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IRoomMessageRepository _roomMessageRepository;
    private readonly MessageService _messageService;

    public MessageServiceTests()
    {
        _roomMessageRepository = Substitute.For<IRoomMessageRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _messageService = new MessageService(_roomMessageRepository, _userRepository);
    }

    [Fact]
    public async Task PostMessage_ShouldReturnTrue_WhenValidParameters()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var roomId = Guid.NewGuid().ToString();
        var createdAt = DateTime.UtcNow;
        const string text = "Test message";
        _roomMessageRepository.Add(Arg.Any<RoomMessage>()).Returns(true);
        
        // Act
        var result = await _messageService.PostMessage(userId, roomId, createdAt, text);
        
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task PostMessage_ShouldReturnFalse_WhenCommandMessage()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var roomId = Guid.NewGuid().ToString();
        var createdAt = DateTime.UtcNow;
        const string text = "/Test message";
        
        // Act
        var result = await _messageService.PostMessage(userId, roomId, createdAt, text);
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task PostMessage_ShouldReturnFalse_WhenInvalidUserId()
    {
        // Arrange
        var userId = "asdasd";
        var roomId = Guid.NewGuid().ToString();
        var createdAt = DateTime.UtcNow;
        const string text = "Test message";
        
        // Act
        var result = await _messageService.PostMessage(userId, roomId, createdAt, text);
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task PostMessage_ShouldReturnFalse_WhenInvalidRoomId()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var roomId = "123";
        var createdAt = DateTime.UtcNow;
        const string text = "Test message";
        
        // Act
        var result = await _messageService.PostMessage(userId, roomId, createdAt, text);
        
        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetMessages_ShouldReturnMessages_WhenValidRoomId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userName = Guid.NewGuid().ToString();
        var roomId = Guid.NewGuid().ToString();
        _roomMessageRepository.GetAllFromRoom(Guid.Parse(roomId), 50)
            .Returns(new List<RoomMessage>{new()
            {
                Message = "Test message",
                UserId = userId
            }});
        _userRepository.GetUserName(userId).Returns(userName);
        
        // Act
        var result = await _messageService.GetMessages(roomId);

        // Assert
        result.Should().HaveCount(1);
        result.First().UserName.Should().Be(userName);
    }
}
