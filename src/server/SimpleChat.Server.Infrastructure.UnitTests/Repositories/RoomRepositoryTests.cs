using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SimpleChat.Server.Domain.Models;
using SimpleChat.Server.Infrastructure.UnitTests.Mocks;
using SimpleChat.Server.Repository.Data;
using SimpleChat.Server.Repository.Repositories;
using Xunit;

namespace SimpleChat.Server.Infrastructure.UnitTests.Repositories;

public class RoomRepositoryTests
{
    private readonly RoomRepository _roomRepository;
    private readonly ApplicationDbContext _context;

    public RoomRepositoryTests()
    {
        var logger = Substitute.For<ILogger<RoomRepository>>();
        _context = ApplicationDbContextMock.Get();
        _roomRepository = new RoomRepository(_context, logger);
    }

    [Fact]
    public async Task Add_ShouldReturnGuid_WhenValidRoomParameters()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const string roomName = "room";

        // Act
        var result = await _roomRepository.Add(userId, roomName);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Add_ShouldReturnNull_WhenInvalidParameters()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var result = await _roomRepository.Add(userId, null);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByRoomId_ShouldReturnRoom_WhenRoomIdExists()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _context.Rooms.Add(new Room
        {
            Id = roomId,
            Name = "name",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _roomRepository.Get(roomId);

        // Assert
        result?.Id.Should().Be(roomId);
    }

    [Fact]
    public async Task GetByRoomId_ShouldReturnNull_WhenRoomIdDoesNotExist()
    {
        // Arrange
        var roomId = Guid.NewGuid();

        // Act
        var result = await _roomRepository.Get(roomId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByName_ShouldReturnRoom_WhenRoomExists()
    {
        // Arrange
        var roomName = Guid.NewGuid().ToString();
        _context.Rooms.Add(new Room
        {
            Id = Guid.NewGuid(),
            Name = roomName,
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _roomRepository.Get(roomName);

        // Assert
        result?.Name.Should().Be(roomName);
    }

    [Fact]
    public async Task GetByName_ShouldReturnNull_WhenRoomNameDoesNotExist()
    {
        // Arrange
        var roomName = Guid.NewGuid().ToString();

        // Act
        var result = await _roomRepository.Get(roomName);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Remove_ShouldReturnTrue_WhenRoomIdExists()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _context.Rooms.Add(new Room
        {
            Id = roomId,
            Name = "name",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _roomRepository.Remove(roomId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Remove_ShouldReturnFalse_WhenRoomIdDoesNotExist()
    {
        // Arrange
        var roomId = Guid.NewGuid();

        // Act
        var result = await _roomRepository.Remove(roomId);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllRooms_WhenTheRoomsExist()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _context.Rooms.Add(new Room
        {
            Id = roomId,
            Name = "name",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _roomRepository.GetAll();

        // Assert
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmpty_WhenTheRoomsDontExist()
    {
        // Act
        var result = await _roomRepository.GetAll();

        // Assert
        result.Should().HaveCount(0);
    }
}
