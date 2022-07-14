using FluentAssertions;
using iot.Domain.Entities.Identity;
using System;
using Xunit;

namespace iot.Domain.UnitTests.Entities;

public class UserTests
{
    private readonly User model;

    public UserTests()
    {
        model = new User(email: "test@gmail.com", phoneNumber: "09124133486", password: "123456",
            name: "testName", surname: "testSurname");
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