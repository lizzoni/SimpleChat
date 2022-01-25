using System;
using System.Threading.Tasks;
using NSubstitute;
using SimpleChat.Bot.Application.Interfaces;
using SimpleChat.Bot.Application.Services;
using SimpleChat.Bot.Domain.Interfaces;
using Xunit;

namespace SimpleChat.Bot.Application.UnitTests.Services;

public class StooqCommandServiceTests
{
    private readonly IMessageBroker _messageBroker;
    private readonly IStooqService _stooqService;
    private readonly StooqCommandService _stooqCommandService;

    public StooqCommandServiceTests()
    {
        _messageBroker = Substitute.For<IMessageBroker>();
        _stooqService = Substitute.For<IStooqService>();
        
        _stooqCommandService = new StooqCommandService(_messageBroker, _stooqService);
    }
    
    [Fact]
    public async Task AddCommand_ShouldAddHook_WhenCommandAdded()
    {
        // Arrange
        var url = "http://localhost:8080/stooq";
        var accessToken = Guid.NewGuid().ToString();
        var roomId = Guid.NewGuid().ToString();
        
        // Act
        await _stooqCommandService.AddCommand(url, accessToken, roomId);

        // Assert
        await _messageBroker.Received(1).AddHook(url, accessToken, roomId, Arg.Any<Func<string, Task<string>>>());
    }
    
}
