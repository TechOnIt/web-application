using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace iot.Domain.ValueObjects;

public class PasswordHash : ValueObject
{
    public PasswordHash()
    {

    }

    // Minimum password character length.
    private static int _minimumLength = 6;
    // Maximum password character length.
    private static int _maximumLength = 50;

    #region Constructors
    public PasswordHash(string password)
    {
        string validatedPassword = _validation(password);
        Value = _encode(validatedPassword);
    }
    #endregion

    public string Value { get; private set; }

    #region Methods
    public static PasswordHash Parse(string password)
    {
        return new PasswordHash(password);
    }

    private static string _validation(string password)
    {
        // Validation
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException("Password cannot be null or empty.");
        if (password.Length < _minimumLength)
            throw new ArgumentOutOfRangeException($"Password characters cannot be less than {_minimumLength}.");
        if (password.Length > _maximumLength)
            throw new ArgumentOutOfRangeException($"Password characters cannot be more than {_maximumLength}.");

        // initializing
        return password;
    }
    private static string _encode(string password)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(password);
        encodedBytes = md5.ComputeHash(originalBytes);
        return BitConverter.ToString(encodedBytes);
    }

    public override int GetHashCode() => Value.GetHashCode();
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion

    #region Operators
    public static bool operator ==(PasswordHash left, PasswordHash right) => left.Value == right.Value;
    public static bool operator !=(PasswordHash left, PasswordHash right) => left.Value != right.Value;
    #endregion
}