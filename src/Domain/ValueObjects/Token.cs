using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class Token : ValueObject
{
    #region Constructors
    Token() { }
    #endregion

    public string Value { get; private set; }

    #region Methods
    public static Token CreateNew()
    {
        var instance = new Token();
        instance.Value = Guid.NewGuid().ToString("N").Substring(0, 12);
        return instance;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion
}