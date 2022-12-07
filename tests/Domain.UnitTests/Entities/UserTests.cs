using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.UnitTests.Entities;

public class UserTests
{
    private readonly User model;

    public UserTests()
    {
        model = User.CreateNewInstance(email: "test@gmail.com", phoneNumber: "09124133486");
        model.SetPassword(PasswordHash.Parse("123456"));
        model.SetFullName(new FullName(name: "testName", surname: "testSurname"));
    }

    [Fact]
    public void UserName_Should_Equal_To_PhoneNumber()
    {
        // Arrange
        var user = User.CreateNewInstance(email: model.Email, model.PhoneNumber);
        user.SetPassword(PasswordHash.Parse("123456"));

        // Assert
        user.Username.Should().Be(user.PhoneNumber);
    }

    [Fact]
    public void Concurrency_token_should_change_on_new()
    {
        // Arrange
        var user = User.CreateNewInstance(email: "test@gmail.com", phoneNumber: "09124133486");
        user.SetPassword(PasswordHash.Parse("123456"));
        user.SetFullName(new FullName(name: "testName", surname: "testSurname"));

        string oldConcurrencyToken = user.ConcurrencyStamp.Value;

        user = User.CreateNewInstance(email: "test@gmail.com", phoneNumber: "09124133486");
        user.SetPassword(PasswordHash.Parse("123456"));
        user.SetFullName(new FullName(name: "testName", surname: "testSurname"));

        string newConcurrencyToken = user.ConcurrencyStamp.Value;

        // Assert
        oldConcurrencyToken.Should().NotBe(newConcurrencyToken);
    }
}