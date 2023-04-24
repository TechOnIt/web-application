using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Common.Models.ViewModels.Users;

public class UserViewModel
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string? Email { get; set; }
    public bool ConfirmedEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public bool ConfirmedPhoneNumber { get; set; }
    public FullName? FullName { get; set; }
    public string RegisteredDateTime { get; set; }
    public bool IsBaned { get; set; }
    public bool IsDeleted { get; set; }
    public string ConcurrencyStamp { get; set; }
}