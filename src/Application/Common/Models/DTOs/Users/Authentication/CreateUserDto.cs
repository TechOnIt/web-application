namespace TechOnIt.Application.Common.Models.DTOs.Users.Authentication;

public class CreateUserDto
{
    public CreateUserDto(string username, string phoneNumber,
        string? password = null, string? email = null,
        string? name = null, string? surname = null)
    {
        Username = username;
        PhoneNumber = phoneNumber;
        Password = password;
        Email = email;
        Name = name;
        Surname = surname;
    }

    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}