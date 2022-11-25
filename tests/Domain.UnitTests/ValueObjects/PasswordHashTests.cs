using Shouldly;

namespace iot.Domain.UnitTests.ValueObjects;

public class PasswordHashTests
{
    #region Correct
    [Theory]
    [InlineData("123456")]
    [InlineData("aaabbb")]
    [InlineData("QQQQQQ")]
    [InlineData("Aa1234")]
    [InlineData("@#$Fg5")]
    [InlineData("pT6ec$5#cdA")]
    public void Should_Can_Set_By_Parse(string password)
    {
        // Arrange
        var passwordHash = PasswordHash.Parse(password);

        // Assert
        passwordHash.Value.Should().NotBe(null);
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("abcDefg")]
    [InlineData("aB8%h*9T")]
    public void Should_Verify_Password_After_Hash(string password)
    {
        // arrange
        var hashedPassword = PasswordHash.Parse(password);

        // act
        bool verifyResult = hashedPassword.VerifyPasswordHash(password);

        // Assert
        verifyResult.ShouldBe(true);
    }
    #endregion

    #region error & exception
    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData("1234")]
    [InlineData("a%H5s")]
    public void Should_Not_Be_Less_Than_Six(string password)
    {
        var exception = Record.Exception(() => PasswordHash.Parse(password));

        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
    }

    [Theory]
    [InlineData("12345678912345678912345678912345678912345678912345u")]
    [InlineData("Mcbjhger%275sgsDgef^df%2kf88nLgr*sa7kcbds5DfDj3bDfaefevffbr")]
    [InlineData("123456789123456789123456789123456789123456789123456789123456789123456789")]
    public void Should_Not_Be_More_Than_Fifty(string password)
    {
        var exception = Record.Exception(() => PasswordHash.Parse(password));

        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
    }

    [Theory]
    [InlineData("sdkoeh", "nkjbrS")]
    public void Should_Not_Equal_With_Diffrent_Value(string oldPassword, string newPassword)
    {
        // Arrange
        var oldPasswordHash = PasswordHash.Parse(oldPassword);
        var newPasswordHash = PasswordHash.Parse(newPassword);

        // Assert
        oldPasswordHash.Should().Match<PasswordHash>(passwordHash => passwordHash != newPasswordHash);
        oldPasswordHash.Should().Match<PasswordHash>(passwordHash => !passwordHash.Equals(newPasswordHash));
    }
    #endregion
}