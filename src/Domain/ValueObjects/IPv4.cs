using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class IPv4 : ValueObject
{
    #region Constructors
    public IPv4() { }

    public IPv4(byte firstOct, byte secondOct, byte thirdOct, byte fourthOct)
    {
        FirstOct = firstOct;
        SecondOct = secondOct;
        ThirdOct = thirdOct;
        FourthOct = fourthOct;
    }
    #endregion

    public byte FirstOct { get; set; } = 0;
    public byte SecondOct { get; set; } = 0;
    public byte ThirdOct { get; set; } = 0;
    public byte FourthOct { get; set; } = 0;

    #region Methods
    public static IPv4 Parse(string address)
    {
        string[] ipScopes = address.Split('.');
        return new IPv4(byte.Parse(ipScopes[0]), byte.Parse(ipScopes[1]), byte.Parse(ipScopes[2]), byte.Parse(ipScopes[3]));
    }
    public override string ToString() => $"{FirstOct}.{SecondOct}.{ThirdOct}.{FourthOct}";

    private string _validation(string ipAddress)
    {
        // Validation


        return ipAddress;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstOct;
    }
    #endregion
}