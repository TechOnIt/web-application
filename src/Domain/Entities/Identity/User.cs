using iot.Domain.Common;
using iot.Domain.Entities.Product;
using iot.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace iot.Domain.Entities.Identity;

public class User : IEntity
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
        return instance; ;
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
    #endregion

    #region relations
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<Structure> Structures { get; set; }
    #endregion
}