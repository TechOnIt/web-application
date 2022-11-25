using iot.Domain.Common;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
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
        => new PasswordHash(password);

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

    public override int GetHashCode() => Value.GetHashCode();
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion

    #region Operators
    public static bool operator ==(PasswordHash left, PasswordHash right)
    {
        //https://stackoverflow.com/questions/4219261/overriding-operator-how-to-compare-to-null
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(PasswordHash left, PasswordHash right)
    {
        if (left is null)
        {
            return right is not null;
        }

        return left.Value != right.Value;
    }
    #endregion

    #region highe level encryption

    public static string _encode(string userPassword)
    {
        var saltBytes = Generate128BitSalt();
        var hashPasswordBytes =
            KeyDerivation.Pbkdf2(
                password: userPassword,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);

        return String.Concat(
            Convert.ToBase64String(saltBytes),
            " ",
            Convert.ToBase64String(hashPasswordBytes));
    }

    public bool VerifyPasswordHash(string password)
    {
        var splitHash = this.Value.Split(" ");

        var saltHash = Convert.FromBase64String(splitHash[0]);
        var passwordHashed = Convert.FromBase64String(splitHash[1]);

        var newhashPasswordBytes =
            KeyDerivation.Pbkdf2(
            password: password,
            salt: saltHash,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8);

        return passwordHashed.SequenceEqual(newhashPasswordBytes);
    }

    private static byte[] Generate128BitSalt()
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return salt;
    }
    #endregion
}