using System;
using System.Linq;
using FluentAssertions;
using SimpleChat.Server.Repository.Notifications;
using Xunit;

namespace SimpleChat.Server.Infrastructure.UnitTests.Notifications;

public class NotificationContextTests
{
    private readonly NotificationContext _notification;

    public NotificationContextTests()
    {
        _notification = new NotificationContext();
    }
    
    [Fact]
    public void AddNotification_ShouldAddNotification_WhenValidParameters()
    {
        // Arrange
        var message = Guid.NewGuid().ToString();
        var detail = Guid.NewGuid().ToString();
        
        // Act
        _notification.AddNotification(message, detail);
        
        // Assert
        _notification.IsValid.Should().BeFalse();
        var notification = _notification.Notifications.First();
        notification.Message.Should().Be(message);
        notification.Detail.Should().Be(detail);
    }
}
