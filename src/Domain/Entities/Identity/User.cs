using System;
using System.Security.Cryptography;
using System.Text;

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
            Password = EncodePassword(password); // Encode password as MD5.
            ConcurrencyStamp =

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

        private string _password;
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                    _password = Password;

                return _password;
            }
            set
            {
                Password = EncodePassword(value);
            }
        }

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
        public bool IsBaned { get; set; }
        public bool IsDeleted { get; set; }
        public short MaxFailCount { get; set; }
        public DateTime? LockOutDateTime { get; set; }

        private void GenerateSecurityStamp()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N").Substring(0, 10);
        }

        private void SetPassword(string password)
        {
            Password = password;
        }

        private string EncodePassword(string Password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(Password);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}