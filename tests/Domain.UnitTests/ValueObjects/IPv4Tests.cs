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
        initIp.ToString().Should().Be("0.0.0.0");
    }

    [Theory]
    [InlineData("1.0.0.0")]
    [InlineData("192.168.1.1")]
    [InlineData("5.115.52.208")]
    [InlineData("89.39.104.238")]
    [InlineData("185.180.222.151")]
    [InlineData("185.165.241.171")]
    public void Should_parse_dotted_decimal_ip(string ipAddress)
    {
        // Arrange
        var ip = IPv4.Parse(ipAddress);

        // Assert
        ip.ToString().Should().Be(ipAddress);
    }

    [Fact]
    public void Should_equals_with_same_type_and_value()
    {
        // Arrange
        var first = IPv4.Parse("192.168.1.1");
        var second = IPv4.Parse("192.168.1.1");

        // Assert
        first.Should().Match<IPv4>(first => first == second);
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
        var exception = Record.Exception(() => IPv4.Parse(ipAddress));

        // Assert
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);

    }

    [Theory]
    [InlineData(192, 168, 1, 1, "192.168.1.1")]
    [InlineData(86, 39, 104, 238, "86.39.104.238")]
    public void Sould_convert_to_string(byte firstOct, byte secondOct, byte thirdOct, byte fourthOct, string ipAddress)
    {
        // Arrange
        var ipV4 = new IPv4(firstOct, secondOct, thirdOct, fourthOct);

        // Assert
        ipV4.ToString().Should().Be(ipAddress);
    }
    #endregion
}