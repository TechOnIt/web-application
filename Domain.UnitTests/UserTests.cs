using iot.Domain.Entities.Identity;
using Moq;
using Shouldly;
using System;
using System.Reflection;
using Xunit;

namespace Domain.UnitTests
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
            user.Username.ShouldBe(user.PhoneNumber);
        }

        [Fact]
        public void Id_Should_Not_Be_Null_If_Not_Define_In_Constructor()
        {
            // Arrange
            var user = new User();

            // Assert
            user.Id.ShouldNotBe(Guid.Empty);
        }
    }
}
