using iot.Domain.Common;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class IPv4 : ValueObject
{
    #region Constructors

    public IPv4(byte firstOct, byte secondOct, byte thirdOct, byte fourthOct)
    {
        FirstOct  = firstOct;
        SecondOct = secondOct;
        ThirdOct  = thirdOct;
        FourthOct = fourthOct;
    }

    public IPv4()
    {
        FirstOct= 0;
        SecondOct= 0;
        ThirdOct= 0;
        FourthOct = 0;
    }
    #endregion

    public byte FirstOct { get; private set; }
    public byte SecondOct { get; private set; } 
    public byte ThirdOct { get; private set; } 
    public byte FourthOct { get; private set; } 

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