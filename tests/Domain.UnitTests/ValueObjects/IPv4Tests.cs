using iot.Domain.ValueObjects;

namespace iot.Domain.UnitTests.ValueObjects;

public class IPv4Tests
{
    [Fact]
    public void Should_have_init_value()
    {
        // Arrange
        var initIp = new IPv4();

        // Assert
        initIp.Address.Should().Be("0.0.0.0");
    }

    [Theory]
    [InlineData("1.0.0.0")]
    [InlineData("192.168.1.1")]
    [InlineData("5.115.52.208")]
    [InlineData("89.39.104.238")]
    [InlineData("185.180.222.151")]
    [InlineData("185.165.241.171")]
    public void should_parse_dotted_decimal_ip(string ipAddress)
    {
        // Arrange
        var ip = IPv4.Parse(ipAddress);

        // Assert
        ip.Address.Should().Be(ipAddress);
    }

    #region errors & exceptions
    [Theory]
    [InlineData("192")]
    [InlineData("192.168")]
    [InlineData("192.168.1")]
    [InlineData("192.168.1.1234")]
    public void Should_validate_address(string ipAddress)
    {
        // Arrange
        var exception = Record.Exception(() => PasswordHash.Parse(ipAddress));

        // Assert
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);

    }
    #endregion
}