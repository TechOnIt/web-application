using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class FullName : ValueObject
{

    #region Constructors
    public FullName() { }
    public FullName(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
    #endregion

    public string Name { get; set; }
    public string Surname { get; set; }

    public string GetName() => Name;
    public string GetSurname() => Surname;
    public string GetFullName() => $"{Name} {Surname}";

    #region Overrides
    public static bool operator ==(FullName left, FullName right) => left.ToString() == right.ToString();
    public static bool operator !=(FullName left, FullName right) => left.ToString() != right.ToString();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Surname);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        bool result = default;
        if (ReferenceEquals(this, obj))
        {
            result =true;
        }

        return result;
    }
    #endregion
}