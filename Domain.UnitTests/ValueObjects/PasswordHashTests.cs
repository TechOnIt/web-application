using iot.Domain.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace iot.Domain.UnitTests.ValueObjects
{
    public class PasswordHashTests
    {
        [Theory]
        [InlineData("123456")]
        [InlineData("aaaaaa")]
        [InlineData("QQQQQQ")]
        [InlineData("Aa1234")]
        [InlineData("@#$Fg5")]
        public void Password_Should_Can_Set_By_Parse(string password)
        {
            // Arrange
            var passwordHash = PasswordHash.Parse(password);

            // Assert
            passwordHash.Value.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("1234")]
        public void Password_Should_Not_Be_Less_Than_Six(string password)
        {
            var exception = Record.Exception(() => PasswordHash.Parse(password));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
        }

        [Theory]
        [InlineData("123456789123456789123456789123456789123456789123456789123456789123456789")]
        public void Password_Should_Not_Be_More_Than_Fifty(string password)
        {
            var exception = Record.Exception(() => PasswordHash.Parse(password));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
        }
    }
}
