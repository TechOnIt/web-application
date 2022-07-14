using iot.Domain.ValueObjects;
using System;

namespace iot.Domain.Entities.Identity
{
    public class User
    {
        #region Constructors
        public User() { }

        public User(string email, string phoneNumber, string password,
            Guid? id = null, string surname = null, string name = null)
        {
            Id = id ?? Guid.NewGuid();
            Username = phoneNumber;
            Email = email.ToLower().Trim();
            ConfirmedEmail = false;
            PhoneNumber = phoneNumber;
            ConfirmedPhoneNumber = false;
            Password = PasswordHash.Parse(password); // Hash the password.
            ConcurrencyStamp = "";

            Name = name;
            Surname = surname;
            MaxFailCount = 0;
            RegisteredDateTime = DateTime.Now;
            IsBaned = false;
            IsDeleted = false;

            GenerateSecurityStamp();
        }
        #endregion

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; private set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }

        public PasswordHash Password { get; private set; }

        public DateTime RegisteredDateTime { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_phoneNumber))
                    return PhoneNumber;
                else
                    return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                Username = value;
            }
        }


        public bool ConfirmedPhoneNumber { get; set; }
        public string ConcurrencyStamp { get; private set; }
        public bool IsBaned { get; private set; }
        public bool IsDeleted { get; private set; }
        public short MaxFailCount { get; private set; }
        public DateTime? LockOutDateTime { get; private set; }

        #region Methods
        public void ConfirmEmail()
        {
            ConfirmedEmail = true;
        }
        public void ConfirmPhoneNumber()
        {
            ConfirmedPhoneNumber = true;
        }
        public void Ban()
        {
            IsBaned = true;
        }
        public void UnBan()
        {
            IsBaned = false;
        }
        public void DeleteAccount()
        {
            IsDeleted = true;
        }
        public void SetLockOut(DateTime lockoutUntill)
        {
            if (lockoutUntill <= DateTime.Now)
                throw new ArgumentOutOfRangeException("lockout cannot be in the past or present!");
            LockOutDateTime = lockoutUntill;
        }
        public void RemoveLockout()
        {
            MaxFailCount = 0;
            LockOutDateTime = null;
        }
        public void IncreaseMaxFailCount()
        {
            MaxFailCount++;
        }

        private void GenerateSecurityStamp()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N").Substring(0, 10);
        }
        #endregion
    }
}