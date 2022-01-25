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

public class RoomMessageRepositoryTests
{
    private readonly RoomMessageRepository _roomMessageRepository;
    private readonly ApplicationDbContext _context;

    public RoomMessageRepositoryTests()
    {
        var logger = Substitute.For<ILogger<RoomMessageRepository>>();
        _context = ApplicationDbContextMock.Get();
        _roomMessageRepository = new RoomMessageRepository(_context, logger);
    }

    [Fact]
    public async Task Add_ShouldReturnTrue_WhenValidMessage()
    {
        // Arrange
        var roomMessage = new RoomMessage
        {
            Id = Guid.NewGuid(),
            Message = "message",
            CreatedAt = DateTime.UtcNow,
            RoomId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act
        var result = await _roomMessageRepository.Add(roomMessage);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Add_ShouldReturnFalse_WhenInvalidMessage()
    {
        // Arrange
        var roomMessage = new RoomMessage();

        // Act
        var result = await _roomMessageRepository.Add(roomMessage);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetAllFromRoom_ShouldReturnEmpty_WhenRoomExistsAndNotHaveMessages()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        const int count = 50;

        // Act
        var result = _roomMessageRepository.GetAllFromRoom(roomId, count);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllFromRoom_ShouldReturnMessages_WhenRoomExistsAndHaveMessages()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        const int count = 50;
        _context.RoomMessages.Add(new RoomMessage
        {
            RoomId = roomId,
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Message = "message",
            UserId = Guid.NewGuid()
        });
        await _context.SaveChangesAsync();

        // Act
        var result = _roomMessageRepository.GetAllFromRoom(roomId, count);

        // Assert
        result.Should().HaveCount(1);
    }
}
