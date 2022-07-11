using FluentAssertions;
using iot.Domain.Entities.Identity;
using System;
using Xunit;

namespace iot.Domain.UnitTests.Entities
{
    public class UserTests
    {
        private readonly User model;

        public UserTests()
        {
            model = new User
            {
                Email = "test@gmail.com",
                PhoneNumber = "09124133486",
                Name = "testName",
                Surname = "testSurname",
                ConfirmedPhoneNumber = false,
                MaxFailCount = 0,
                ConfirmedEmail = false,
                IsBaned = false,
                IsDeleted = false,
            };
        }

        [Fact]
        public void UserName_Should_Equal_To_PhoneNumber()
        {
            // Arrange
            var user = new User();

            // Act
            user.PhoneNumber = model.PhoneNumber;

            // Assert
            user.Username.Should().Be(user.PhoneNumber);
        }

        [Fact]
        public void Id_Should_Not_Be_Null_If_Not_Define_In_Constructor()
        {
            // Arrange
            var user = new User();

            // Assert
            user.Id.Should().NotBe(Guid.Empty);
        }
    }
}