namespace TechOnIt.Domain.ValueObjects;

public class IPv4 : ValueObject
{
    public string? Value { get; set; }

    #region Ctor
    private IPv4() { }
    public IPv4(string ipAddress)
    {
        setValue(ipAddress);
    }
    #endregion

    #region Methods
    private void setValue(string value)
    {
        if (value is null)
        {
            Value = null;
            return;
        }
        if (value.Length < 7) throw new ArgumentException("Invalid Ip address.");
        if (value.Split('.').Length < 4) throw new ArgumentException("Invalid Ip address.");

        Value = value;
    }
    public static IPv4 Parse(string ipAddress)
        => new IPv4(ipAddress);
    public override string ToString() => Value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion

    #region Operators
    public static bool operator ==(IPv4 left, IPv4 right)
        => (left.Value == right.Value);
    public static bool operator !=(IPv4 left, IPv4 right)
        => (left.Value != right.Value);
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (ReferenceEquals(obj, null))
        {
            return false;
        }
        return ((IPv4)obj == this);
    }
    public override int GetHashCode()
        => Value is null ? 0 : Value.GetHashCode();
    #endregion
}