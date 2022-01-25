using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using SimpleChat.Server.Application.Services;
using SimpleChat.Server.Domain.Interfaces;
using SimpleChat.Server.Domain.Models;
using Xunit;

namespace SimpleChat.Server.Application.UnitTests.Services;

public class RoomServiceTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly RoomService _roomService;

    public RoomServiceTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _roomService = new RoomService(_roomRepository);
    }
    
    [Fact]
    public async Task Create_ShouldReturnSucceeded_WhenValidParameters()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var roomName = Guid.NewGuid().ToString();
        _roomRepository.Add(userId, roomName).Returns(roomId);
        
        // Act
        var result = await _roomService.Create(userId, roomName);

        // Assert
        result.Succeeded.Should().BeTrue();
    }
    
    [Fact]
    public async Task Create_ShouldReturnFailed_WhenRoomAlreadyExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roomName = Guid.NewGuid().ToString();
        _roomRepository.Get(roomName).Returns(new Room());
        
        // Act
        var result = await _roomService.Create(userId, roomName);

        // Assert
        result.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmpty_WhenThereAreNoRooms()
    {
        // Act
        var result = await _roomService.GetAll();
        
        // Assert
        result.Should().BeEmpty();
    }
}
