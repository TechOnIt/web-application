using System;

namespace iot.Domain.ValueObjects;

public class IPv4
{
    #region Constructors
    public IPv4() { }

    public IPv4(string address)
    {
        Address = address;
    }
    #endregion

    public string Address { get; private set; }

    public static IPv4 Parse(string address)
    {
        return new IPv4(address);
    }
}