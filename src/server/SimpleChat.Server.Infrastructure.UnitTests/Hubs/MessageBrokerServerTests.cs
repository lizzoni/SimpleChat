using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Repository.Hubs;
using Xunit;

namespace SimpleChat.Server.Infrastructure.UnitTests.Hubs;

public class MessageBrokerServerTests
{
    private readonly MessageBrokerServer _messageBroker;
    private readonly IMessageService _messageService;

    public MessageBrokerServerTests()
    {
        _messageService = Substitute.For<IMessageService>();
        _messageBroker = new MessageBrokerServer(_messageService);
        _messageBroker.Context = Substitute.For<HubCallerContext>();
        _messageBroker.Clients = Substitute.For<IHubCallerClients>();
    }
    
    [Fact]
    public async Task SendMessage_ShouldPostMessage_WhenValidParameters()
    {
        // Arrange
        var roomId = Guid.NewGuid().ToString();
        var createdAt = DateTime.UtcNow;
        var message = "Hello world!";
        
        // Act
        await _messageBroker.SendMessage(roomId, createdAt, message);

        // Assert
        await _messageService.Received(1).PostMessage(Arg.Any<string>(), roomId, createdAt, message);
    }
}
