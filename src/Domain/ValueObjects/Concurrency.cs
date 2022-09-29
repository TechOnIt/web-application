using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iot.Domain.ValueObjects;

public class Concurrency : ValueObject
{
    public Concurrency()
    {

    }

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
    //public static bool operator ==(Concurrency left, Concurrency right) => left.Value == right.Value;
    public static bool operator ==(Concurrency left, Concurrency right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Concurrency left, Concurrency right) => left.Value != right.Value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion
}