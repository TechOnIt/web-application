using iot.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace iot.Domain.UnitTests.ValueObjects
{
    public class PasswordHashTests
    {
        #region Correct
        [Theory]
        [InlineData("123456")]
        [InlineData("aaaaaa")]
        [InlineData("QQQQQQ")]
        [InlineData("Aa1234")]
        [InlineData("@#$Fg5")]
        [InlineData("pT6ec$5#cdA")]
        public void Password_Should_Can_Set_By_Parse(string password)
        {
            // Arrange
            var passwordHash = PasswordHash.Parse(password);

            // Assert
            passwordHash.Value.ShouldNotBeNull();
        }
        #endregion

        #region error & exception
        [Theory]
        [InlineData(null)]
        [InlineData("abc")]
        [InlineData("1234")]
        [InlineData("a%H5s")]
        public void Password_Should_Not_Be_Less_Than_Six(string password)
        {
            var exception = Record.Exception(() => PasswordHash.Parse(password));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
        }

        [Theory]
        [InlineData("12345678912345678912345678912345678912345678912345u")]
        [InlineData("Mcbjhger%275sgsDgef^df%2kf88nLgr*sa7kcbds5DfDj3bDfaefevffbr")]
        [InlineData("123456789123456789123456789123456789123456789123456789123456789123456789")]
        public void Password_Should_Not_Be_More_Than_Fifty(string password)
        {
            var exception = Record.Exception(() => PasswordHash.Parse(password));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
        }
        #endregion
    }
}