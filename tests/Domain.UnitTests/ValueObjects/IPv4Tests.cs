namespace TechOnIt.Domain.UnitTests.ValueObjects;

public class IPv4Tests
{
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
        IPv4 left = IPv4.Parse("192.168.1.1");
        IPv4 right = IPv4.Parse("192.168.1.1");
        // Assert
        left.ShouldBe(right);
    }

    #region errors & exceptions
    [Theory]
    [InlineData("192")]
    [InlineData("192.168")]
    [InlineData("192.168.1")]
    [InlineData("192.168.1.1234")]
    public void Should_validate_address_and_throw_exception(string ipAddress)
    {
        // Arrange
        var exception = Record.Exception(() => IPv4.Parse(ipAddress));

        // Assert
        Assert.NotNull(exception);
    }
    #endregion
}