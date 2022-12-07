using System.Collections.Generic;
using TechOnIt.Domain.Common;

namespace TechOnIt.Domain.ValueObjects;

public class IPv4 : ValueObject
{
    #region Constructors

    public IPv4(byte firstOct, byte secondOct, byte thirdOct, byte fourthOct)
    {
        FirstOct = firstOct;
        SecondOct = secondOct;
        ThirdOct = thirdOct;
        FourthOct = fourthOct;
    }

    public IPv4()
    {
    }
    #endregion

    private byte? _FirstOct;
    private byte? _SecondOct;
    private byte? _ThirdOct;
    private byte? _FourthOct;

    public byte FirstOct
    {
        get { return _FirstOct ?? 0; }
        private set { _FirstOct = value; }
    }

    public byte SecondOct
    {
        get { return _SecondOct ?? 0; }
        private set { _SecondOct = value; }
    }

    public byte ThirdOct
    {
        get { return _ThirdOct ?? 0; }
        private set { _ThirdOct = value; }
    }

    public byte FourthOct
    {
        get { return _FourthOct ?? 0; }
        private set
        {
            _FourthOct = value;
        }
    }

    #region Methods
    public static IPv4 Parse(string address)
    {
        string[] ipScopes = address.Split('.');
        return new IPv4(byte.Parse(ipScopes[0]), byte.Parse(ipScopes[1]), byte.Parse(ipScopes[2]), byte.Parse(ipScopes[3]));
    }
    public override string ToString() => $"{FirstOct}.{SecondOct}.{ThirdOct}.{FourthOct}";
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstOct;
    }
    #endregion

    #region Operators
    public static bool operator ==(IPv4 left, IPv4 right)
    {
        if (left.FirstOct == right.FirstOct &&
            left.SecondOct == right.SecondOct &&
            left.ThirdOct == right.ThirdOct &&
            left.FourthOct == right.FourthOct) return true;
        else
            return false;
    }

    public static bool operator !=(IPv4 left, IPv4 right)
    {
        if (left.FirstOct != right.FirstOct ||
            left.SecondOct != right.SecondOct ||
            left.ThirdOct != right.ThirdOct ||
            left.FourthOct != right.FourthOct) return true;
        else
            return false;
    }
    #endregion
}