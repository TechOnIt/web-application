using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iot.Domain.ValueObjects;

public class Concurrency : ValueObject
{
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
    Concurrency() { }

    public string Value { get; set; }

    public static Concurrency NewToken()
    {
        var instance = new Concurrency();
        instance.Value = instance.GenerateToken();
        return instance;
    }

    private string GenerateToken(int count = 16)
    {
        var random = new Random();
        return new string(Enumerable.Repeat(_chars, count)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}