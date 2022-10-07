namespace iot.Application.Common.Models;

// I Removed {} for Namespace because we its better to do in c# 10 and .net 6
// https://dotnetcoretutorials.com/2021/09/20/file-scoped-namespaces-in-c-10/

public class UserViewModel
{

    public UserViewModel()
    {

    }

    public UserViewModel(Guid id, string? username, FullName fullname, FullName fullName, string? name, string? surname, string? email, string? phoneNumber, bool confirmedEmail, bool confirmedPhoneNumber, DateTime registeredDateTime, DateTime? lockOutDateTime, bool isBaned, bool isDeleted, short maxFailCount, Concurrency concurrencyStamp, PasswordHash password)
    {
        Id = id;
        Username = username;
        _fullname = fullname;
        FullName = fullName;
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        ConfirmedEmail = confirmedEmail;
        ConfirmedPhoneNumber = confirmedPhoneNumber;
        RegisteredDateTime = registeredDateTime;
        LockOutDateTime = lockOutDateTime;
        IsBaned = isBaned;
        IsDeleted = isDeleted;
        MaxFailCount = maxFailCount;
        ConcurrencyStamp = concurrencyStamp;
        Password = password;
    }

    public UserViewModel(string username, string phonenumber, string password)
    {
        this.Username = username;
        this.PhoneNumber = phonenumber;
        this.Password = PasswordHash.Parse(password);
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
    public UserViewModel(string username,string phonenumber,string password,string name,string surname,string email="")
    {
        this.Username = username;
        this.PhoneNumber = phonenumber;
        this.Password = PasswordHash.Parse(password);
        this.Email = email;
        this.ConcurrencyStamp = Concurrency.NewToken();
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
            if(_fullname is null)
                _fullname = new FullName(this.Name, this.Surname);

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
    public string? PhoneNumber {get;private set;}
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
        this.PhoneNumber = phonenumber;
    }

    public void SetEmail(string email)
    {
        this.Email = email;
    }
}
