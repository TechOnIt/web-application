using iot.Domain.ValueObjects;
using System;

namespace iot.Domain.Entities.Identity
{
    public class User
    {
        #region Constructors
        public User()
        {
            Id = Guid.NewGuid();
            RegisteredDateTime = DateTime.Now;
            ConcurrencyStamp = Token.CreateNew(); // Create new stamp.
        }

        public User(string email, string phoneNumber, string password,
            string surname = null, string name = null)
        {
            Id = Guid.NewGuid();
            SetEmail(email);
            SetPhoneNumber(phoneNumber); // Also set phone number in username.
            Password = PasswordHash.Parse(password); // Hash the password.

            Name = name;
            Surname = surname;
            MaxFailCount = 0;
            RegisteredDateTime = DateTime.Now;
            IsBaned = false;
            IsDeleted = false;
            ConcurrencyStamp = Token.CreateNew(); // Create new stamp.
        }
        #endregion

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; private set; }
        public PasswordHash Password { get; private set; }
        public string Email { get; private set; }
        public bool ConfirmedEmail { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool ConfirmedPhoneNumber { get; private set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegisteredDateTime { get; private set; }
        public Token ConcurrencyStamp { get; private set; }
        public bool IsBaned { get; set; }
        public bool IsDeleted { get; set; }
        public short MaxFailCount { get; private set; }
        public DateTime? LockOutDateTime { get; private set; }

        #region Methods
        public void SetEmail(string email)
        {
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
    }
}