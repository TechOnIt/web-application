using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class Token : ValueObject
{
    #region Constructors
    Token() => Value = _generateToken();
    #endregion

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
        return Guid.NewGuid().ToString("N").Substring(0, 12);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion
}