namespace iot.Domain.UnitTests.Entities;

public class UserTests
{
    private readonly User model;

    public UserTests()
    {
        model = new User(email: "test@gmail.com", phoneNumber: "09124133486",
            passwordHash: PasswordHash.Parse("123456"),
            name: "testName", surname: "testSurname");
    }

    [Fact]
    public void UserName_Should_Equal_To_PhoneNumber()
    {
        // Arrange
        var user = new User(email: model.Email, phoneNumber: model.PhoneNumber,
            passwordHash: PasswordHash.Parse("123456"));

        // Assert
        user.Username.Should().Be(user.PhoneNumber);
    }

    [Fact]
    public void Concurrency_token_should_change_on_new()
    {
        // Arrange
        var user = new User(email: "test@gmail.com", phoneNumber: "09124133486",
            passwordHash: PasswordHash.Parse("123456"),
            name: "testName", surname: "testSurname");
        string oldConcurrencyToken = user.ConcurrencyStamp.Value;

        user = new User(email: "test@gmail.com", phoneNumber: "09124133486",
            passwordHash: PasswordHash.Parse("123456"),
            name: "testName", surname: "testSurname");
        string newConcurrencyToken = user.ConcurrencyStamp.Value;

        // Assert
        oldConcurrencyToken.Should().NotBe(newConcurrencyToken);
    }
}