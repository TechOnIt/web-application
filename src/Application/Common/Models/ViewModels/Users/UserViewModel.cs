namespace iot.Application.Common.Models.ViewModels.Users;

public class UserViewModel
{

    public UserViewModel()
    {

    }

    public UserViewModel(string username, string phonenumber, string password)
    {
        Username = username;
        PhoneNumber = phonenumber;
        Password = PasswordHash.Parse(password);
        ConfirmedEmail = false;
        ConfirmedPhoneNumber = false;
        RegisteredDateTime = DateTime.Now;
        IsBaned = false;
        IsDeleted = false;
        MaxFailCount = 3;
    }

    /// <summary>
    /// constructor for sign up
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="phonenumber"></param>
    public UserViewModel(string username, string phonenumber, string? password, string? name, string? surname, string? email = "")
    {
        Username = username;
        PhoneNumber = phonenumber;
        Password = PasswordHash.Parse(password);
        Email = email;
        ConcurrencyStamp = Concurrency.NewToken();
        Name = name;
        Surname = surname;
        ConfirmedEmail = false;
        ConfirmedPhoneNumber = false;
        RegisteredDateTime = DateTime.Now;
        IsBaned = false;
        IsDeleted = false;
        MaxFailCount = 3;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Username { get; private set; }

    private FullName _fullname;
    public FullName FullName
    {
        get
        {
            if (_fullname is null)
                _fullname = new FullName(Name, Surname);

            return _fullname;
        }
        private set
        {
            _fullname = value;
        }
    }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool ConfirmedEmail { get; private set; }
    public bool ConfirmedPhoneNumber { get; private set; }
    public DateTime RegisteredDateTime { get; private set; }
    public DateTime? LockOutDateTime { get; private set; }
    public bool IsBaned { get; set; }
    public bool IsDeleted { get; set; }
    public short MaxFailCount { get; private set; }

    public Concurrency ConcurrencyStamp { get; private set; }
    public PasswordHash Password { get; private set; }

    public void SetOrchangePhonenumber(string phonenumber)
    {
        PhoneNumber = phonenumber;
    }

    public void SetEmail(string email)
    {
        Email = email;
    }
}