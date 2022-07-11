using System;

namespace iot.Domain.ValueObjects
{
    public class PasswordHash
    {
        // Minimum password character length.
        private int _minimumLength = 6;
        // Maximum password character length.
        private int _maximumLength = 50;

        PasswordHash() { }
        PasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Password cannot be null or empty.");
            if (password.Length < _minimumLength)
                throw new ArgumentOutOfRangeException($"Password characters cannot be less than {_minimumLength}.");
            if (password.Length > _maximumLength)
                throw new ArgumentOutOfRangeException($"Password characters cannot be more than {_maximumLength}.");
            Value = password;
        }

        public string Value { get; private set; }

        public static PasswordHash Parse(string password)
        {
            return new PasswordHash(password);
        }
    }
}