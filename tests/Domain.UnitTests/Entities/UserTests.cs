using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.UnitTests.Entities;

public class UserTests
{
    private readonly User model;

    public UserTests()
    {
        model = new User(email: "test@gmail.com", phoneNumber: "09124133486");
        model.SetPassword(PasswordHash.Parse("123456"));
        model.SetFullName(new FullName(name: "testName", surname: "testSurname"));
    }

    [Fact]
    public void UserName_Should_Equal_To_PhoneNumber()
    {
        // Arrange
        var user = new User(email: model.Email, model.PhoneNumber);
        user.SetPassword(PasswordHash.Parse("123456"));

        // Assert
        user.Username.Should().Be(user.PhoneNumber);
    }
}