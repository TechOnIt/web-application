using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iot.Domain.ValueObjects;

public class Concurrency : ValueObject
{
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";

    // all value objcets must be immutable
    // its mean : we cant change properties value without constructor
    public string Value { get; private set; }

    private string GenerateToken(int count = 16)
    {
        var random = new Random();
        return new string(Enumerable.Repeat(_chars, count)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static Concurrency NewToken()
    {
        var instance = new Concurrency();
        instance.Value = instance.GenerateToken();

        return instance;
    }
    public static Concurrency Parse(string stamp)
    {
        var instance = new Concurrency();
        instance.Value = stamp;

        return instance;
    }

    #region Operator's
    public static bool operator ==(Concurrency c1, Concurrency c2) => c1.Value == c2.Value;
    public static bool operator !=(Concurrency c1, Concurrency c2) => c1.Value != c2.Value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion
}