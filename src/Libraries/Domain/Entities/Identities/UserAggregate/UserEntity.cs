﻿using TechOnIt.Domain.Entities.Catalogs;
using TechOnIt.Domain.Entities.Generals;
using TechOnIt.Domain.Entities.Securities;

namespace TechOnIt.Domain.Entities.Identities.UserAggregate;

public class UserEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public bool ConfirmedEmail { get; private set; } = false;
    public string? PhoneNumber { get; private set; }
    public bool ConfirmedPhoneNumber { get; private set; } = false;
    public FullName? FullName { get; private set; }
    public PasswordHash? Password { get; private set; }
    public DateTime RegisteredAt { get; private set; } = DateTime.Now;
    public bool IsBaned { get; private set; } = false;
    public bool IsDeleted { get; private set; } = false;
    public short MaxFailCount { get; private set; } = 0;
    public DateTime? LockOutDateTime { get; private set; }
    public byte[] ConcurrencyStamp { get; private set; } = [];
    #region Relations
    public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }
    public virtual ICollection<StructureEntity>? Structures { get; set; }
    public virtual ICollection<LoginActivityEntity>? LoginHistories { get; set; }
    public virtual ICollection<LogEntity>? LogHistories { get; set; }
    public virtual ICollection<DynamicAccessEntity> DynamicAccesses { get; set; }
    #endregion

    #region Ctor
    UserEntity() { }
    public UserEntity(string email, string phoneNumber)
    {
        GenerateNewId();
        SetEmail(email);
        SetPhoneNumber(phoneNumber);
    }
    public UserEntity(string phoneNumber)
    {
        GenerateNewId();
        SetPhoneNumber(phoneNumber);
        UnBan();
    }
    #endregion

    #region Methods
    private void GenerateNewId()
    {
        Id = Guid.NewGuid();
    }
    public void SetPassword(PasswordHash password)
    {
        Password = password;
    }
    public void SetFullName(FullName fullname)
    {
        FullName = fullname;
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
    public void IncreaseMaxFailCount()
    {
        MaxFailCount++;
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
    public string GenerateNewOtpCode()
    {
        Random random = new Random();
        const string chars = "0123345667899";
        return new string(Enumerable.Repeat(chars, 4)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public void Ban()
    {
        IsBaned = true;
    }
    public void UnBan()
    {
        IsBaned = false;
    }
    public void Delete()
    {
        IsDeleted = true;
    }
    /// <summary>
    /// Check row version is validate?
    /// </summary>
    public bool IsConcurrencyStampValidate(string concurrencyStamp)
        => ConcurrencyStamp == Encoding.ASCII.GetBytes(concurrencyStamp);
    #region Login History Aggregate
    public IList<LoginActivityEntity> GetLoginHistories()
        => LoginHistories.ToList();
    public void AddLoginHistory(LoginActivityEntity loginHistory)
        => LoginHistories.Add(loginHistory);
    public void DeleteLoginHistory(LoginActivityEntity loginHistory)
        => LoginHistories.Remove(loginHistory);
    #endregion
    #endregion
}
