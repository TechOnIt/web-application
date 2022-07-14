using iot.Domain.ValueObjects;

namespace iot.Domain.UnitTests.ValueObjects;

public class TokenTests
{
    [Fact]
    public void Should_not_be_null()
    {
        // Arrange
        var token = Token.CreateNew();

        // Assert
        token.Value.Should().NotBeNullOrEmpty(token.Value);
    }

    [Fact]
    public void Should_have_12_character()
    {
        // Arrange
        var token = Token.CreateNew();

        // Assert
        token.Value.Should().HaveLength(12);
    }
}