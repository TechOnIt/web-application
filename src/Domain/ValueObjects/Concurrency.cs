using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iot.Domain.ValueObjects;

public class Concurrency : ValueObject
{
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
    Concurrency() 
    {
        Value = GenerateToken();
    }

    // all value objcets must be immutable
    // its mean : we cant change properties value without constructor
    public string Value { get; private set; }

    public static Concurrency NewToken() => new Concurrency();
    public void RefreshToken()
    {
        Value = GenerateToken();
    }
    private string GenerateToken(int count = 16)
    {
        var random = new Random();
        return new string(Enumerable.Repeat(_chars, count)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static bool operator ==(Concurrency c1, Concurrency c2) => c1.Value == c2.Value;
    public static bool operator !=(Concurrency c1, Concurrency c2) => c1.Value != c2.Value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}