using iot.Domain.Common;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class FullName : ValueObject
{
    public FullName() { }

    public string Name { get; set; } // First name
    public string Surname { get; set; } // Last name

    public void SetName(string name)
    {

    }

    public void SetSurname(string surname)
    {

    }

    public void SetFullName(string name, string surname)
    {

    }

    #region Overrides
    public static bool operator ==(FullName left, FullName right) => left.ToString() == right.ToString();
    public static bool operator !=(FullName left, FullName right) => left.ToString() != right.ToString();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }
    #endregion
}