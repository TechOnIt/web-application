using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        // Minimum password character length.
        private int _minimumLength = 6;
        // Maximum password character length.
        private int _maximumLength = 50;

        #region Constructors
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
        #endregion

        public string Value { get; private set; }

        public static PasswordHash Parse(string password)
        {
            return new PasswordHash(password);
        }

        #region Operator and Methods
        public static bool operator ==(PasswordHash left, PasswordHash right) => left.Value == right.Value;
        public static bool operator !=(PasswordHash left, PasswordHash right) => left.Value != right.Value;

        public override bool Equals(Object obj) => Value.Equals((obj as PasswordHash).Value);
        public bool NotEquals(Object obj) => !Equals(obj);

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion
    }
}