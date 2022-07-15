using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iot.Domain.ValueObjects;

public class Token : ValueObject
{
    #region Constructors
    Token() => Value = _generateToken();
    #endregion

    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";

    public string Value { get; private set; }

    #region Methods
    public static Token CreateNew()
    {
        var instance = new Token();
        instance.Value = _generateToken();
        return instance;
    }

    private static string _generateToken()
    {
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 16)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public override string ToString() => Value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion
}