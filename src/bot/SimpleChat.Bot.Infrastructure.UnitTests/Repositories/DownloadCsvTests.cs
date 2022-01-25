using System.Threading.Tasks;
using FluentAssertions;
using SimpleChat.Bot.Infrastructure.Repositories;
using Xunit;

namespace SimpleChat.Bot.Infrastructure.UnitTests.Repositories;

public class DownloadCsvTests
{
    private readonly DownloadCsv _downloadCsv;

    public DownloadCsvTests()
    {
        _downloadCsv = new DownloadCsv();
    }
    
    [Fact]
    public async Task GetContent_ShouldReturnContent_WhenValidUrl()
    {
        // Arrange
        const string url = "https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv";

        // Act
        var result = await _downloadCsv.GetContent(url);
            
        // Assert
        result.Should().HaveCount(2);
    }
    
    [Fact]
    public async Task GetContent_ShouldReturnNull_WhenInvalidUrl()
    {
        // Arrange
        const string url = "";

        // Act
        var result = await _downloadCsv.GetContent(url);
            
        // Assert
        result.Should().BeNull();
    }
}
