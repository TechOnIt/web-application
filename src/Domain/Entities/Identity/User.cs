using System;
using System.Security.Cryptography;
using System.Text;

namespace iot.Domain.Entities.Identity
{
    public class User
    {
        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = EncodePassword(password);
            MaxFailCount = 0;
            RegisteredDateTime = DateTime.Now;
            IsBaned = false;
            IsDeleted = false;
        }

        public User(Guid id, string username, string email, string password, string name, string surname,string phoneNumber)
        {
            Id = id;
            Username = username;
            Password = EncodePassword(password);
            Email = email;
            PhoneNumber = phoneNumber;
            Name = name;
            Surname = surname;
            MaxFailCount = 0;
            RegisteredDateTime = DateTime.Now;
            IsBaned =   false;
            IsDeleted = false;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public DateTime RegisteredDateTime { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBaned { get; set; }
        public bool IsDeleted { get; set; }
        public short MaxFailCount { get; set; }
        public DateTime? LockOutDateTime { get; set; }

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
