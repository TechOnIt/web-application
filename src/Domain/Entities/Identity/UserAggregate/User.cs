using System;
using System.Collections.Generic;
using System.Linq;
using TechOnIt.Domain.Entities.Identity;
using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Domain.ValueObjects;

namespace TechOnIt.Domain.Entities.Identity.UserAggregate;

public class User
{
    public User()
    {

    }

    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public PasswordHash Password { get; private set; } // Must be nullable. maybe we use otp in future!
    public string Email { get; private set; }
    public bool ConfirmedEmail { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool ConfirmedPhoneNumber { get; private set; }

    public FullName FullName { get; private set; }
    public DateTime RegisteredDateTime { get; private set; }
    public Concurrency ConcurrencyStamp { get; private set; }
    public bool IsBaned { get; private set; }
    public bool IsDeleted { get; private set; }
    public short MaxFailCount { get; private set; }
    public DateTime? LockOutDateTime { get; private set; }

    #region Methods
    public static User CreateNewInstance(string email, string phoneNumber)
    {
        var instance = new User();
        instance.Id = Guid.NewGuid();
        instance.SetEmail(email);
        instance.SetPhoneNumber(phoneNumber);
        instance.ConcurrencyStamp = Concurrency.NewToken();
        instance.RegisteredDateTime = DateTime.Now;
        instance.IsBaned = false;
        instance.IsDeleted = false;
        instance.MaxFailCount = 0;
        return instance;
    }
    public void SetIsDelete(bool value)
    {
        IsDeleted = value;
    }
    public void RefreshConcurrencyStamp()
    {
        ConcurrencyStamp = Concurrency.NewToken();
    }
    public void SetPassword(PasswordHash password)
    {
        Password = password;
    }
    public void SetFullName(FullName fullname)
    {
        FullName = fullname;
    }
    public void SetIsBaned(bool isBane)
    {
        IsBaned = isBane;
    }
    public void SetEmail(string email)
    {
        if (email.Length < 6)
            throw new ArgumentOutOfRangeException("Email must grater than 3 character.");
        Email = email.Trim().ToLower();
        ConfirmedEmail = false;
    }
    public void ConfirmEmail()
    {
        if (string.IsNullOrEmpty(Email))
            throw new ArgumentNullException("While the email is empty, it cannot be verified.");
        ConfirmedEmail = true;
    }
    public void SetPhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Username = phoneNumber;
        ConfirmedPhoneNumber = false;
    }
    public void ConfirmPhoneNumber()
    {
        if (string.IsNullOrEmpty(PhoneNumber))
            throw new ArgumentNullException("While the phone number is empty, it cannot be verified.");
        ConfirmedPhoneNumber = true;
    }
    public void SetLockOut(DateTime lockoutUntill)
    {
        if (lockoutUntill <= DateTime.Now)
            throw new ArgumentOutOfRangeException("lockout cannot be in the past or present!");
        LockOutDateTime = lockoutUntill;
    }
    public void UnLock()
    {
        MaxFailCount = 0;
        LockOutDateTime = null;
    }
    public void IncreaseMaxFailCount()
    {
        MaxFailCount++;
    }
    public string GenerateNewOtpCode()
    {
        Random random = new Random();
        const string chars = "0123345667899";
        return new string(Enumerable.Repeat(chars, 4)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    #region Login History Aggregate
    public IList<LoginHistory> GetLoginHistories()
        => LoginHistories.ToList();
    public void AddLoginHistory(LoginHistory loginHistory)
        => LoginHistories.Add(loginHistory);
    public void DeleteLoginHistory(LoginHistory loginHistory)
        => LoginHistories.Remove(loginHistory);
    #endregion
    #endregion

    #region Relations
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<Structure> Structures { get; set; }
    public virtual ICollection<LoginHistory> LoginHistories { get; set; }
    #endregion
}