using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using SimpleChat.Bot.Application.Services;
using SimpleChat.Bot.Domain.Interfaces;
using Xunit;

namespace SimpleChat.Bot.Application.UnitTests.Services;

public class StooqServiceTests
{
    private readonly StooqService _stooqService;
    private readonly IDownloadCsv _downloadCsv;

    public StooqServiceTests()
    {
        _downloadCsv = Substitute.For<IDownloadCsv>();
        _stooqService = new StooqService(_downloadCsv);
    }

    [Fact]
    public async Task GetQuoteMessage_ShouldReturnMessage_WhenSymbolExists()
    {
        // Arrange
        const string symbol = "AAPL.US";
        const string quote = "162.41";

        _downloadCsv.GetContent(Arg.Any<string>())
            .Returns(new[] { "Symbol,Date,Time,Open,High,Low,Close,Volume", "AAPL.US,2022-01-21,22:00:09,164.415,166.33,162.3,162.41,122848858" });

        // Act
        var result = await _stooqService.GetQuoteMessage(symbol);

        // Assert
        result.Should().Be($"{symbol} quote is ${quote} per share");
    }
    
    [Fact]
    public async Task GetQuoteMessage_ShouldReturnErrorMessage_WhenSymbolDoesNotExist()
    {
        // Arrange
        const string symbol = "AAPL.US";

        _downloadCsv.GetContent(Arg.Any<string>())
            .Returns(new[] { "Symbol,Date,Time,Open,High,Low,Close,Volume", "AAPL.US,N/D,N/D,N/D,N/D,N/D,N/D,N/D" });

        // Act
        var result = await _stooqService.GetQuoteMessage(symbol);

        // Assert
        result.Should().Be($"There is no quote for \"{symbol}\"");
    }
}
