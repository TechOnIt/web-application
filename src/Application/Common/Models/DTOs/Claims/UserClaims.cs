namespace TechOnIt.Application.Common.Models.DTOs.Claims;

public class UserClaims
{
    public UserClaims(Guid id, string fullname, string phonenumber, string role)
    {
        UserId = id;
        Fullname = fullname;
        PhoneNumber = phonenumber;
        Role = role;
    }

    public string Fullname { get; set; }
    public Guid UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
}
