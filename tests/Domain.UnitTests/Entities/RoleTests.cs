namespace TechOnIt.Domain.UnitTests.Entities;

public class RoleTests
{
    [Theory]
    [InlineData("Admin", "Admin", "admin")]
    [InlineData(" Sample  ", "Sample", "sample")]
    [InlineData("   eXamPle   ", "eXamPle", "example")]
    public void Should_trim_and_normalize_name(string initName, string name, string normalizedName)
    {
        // Arrange
        var role = new Role(initName);

        // Assert
        role.Name.Should().Be(name);
        role.NormalizedName.Should().Be(normalizedName);
    }

    #region errors & exception
    [Theory]
    [InlineData(null)]
    [InlineData("  ")]
    public void Should_throw_null_exception(string name)
    {
        // Arrange
        var exception = Record.Exception(() => new Role(name));

        // Assert
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
    }
    #endregion
}