using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class IPv4 : ValueObject
{
    #region Constructors
    public IPv4() { }

    public IPv4(string address)
    {
        Address = _validation(address);
    }
    #endregion

    public string Address { get; private set; }

    #region Methods
    public static IPv4 Parse(string address)
    {
        return new IPv4(address);
    }

    private static string _validation(string ipAddress)
    {
        // Validation


        return ipAddress;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
    #endregion
}