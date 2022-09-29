using iot.Domain.Common;
using System;
using System.Collections.Generic;

namespace iot.Domain.ValueObjects;

public class FullName : ValueObject
{

    #region Constructors
    public FullName()
    {

    }

    public FullName(string name, string surname)
    {
        if (name.Length > 50)
        {
            throw new ArgumentOutOfRangeException("name can not be more than 50 characters");
        }
        else
        {
            Name = name;
        }

        if (surname.Length > 50)
        {
            throw new ArgumentOutOfRangeException("surname can not be more than 50 characters");
        }
        else
        {
            Surname = surname;
        }
    }
    #endregion

    public string Name { get; private set; }
    public string Surname { get; private set; }

    public string GetName() => Name;
    public string GetSurname() => Surname;
    public string GetFullName() => $"{Name} {Surname}";

    #region Overrides
    //public static bool operator ==(FullName left, FullName right) => $"{left.Name}{left.Surname}" == $"{right.Name}{right.Surname}";
    public static bool operator ==(FullName left, FullName right)
    {
        if (left is null || right is null)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        return $"{left.Name}{left.Surname}" == $"{right.Name}{right.Surname}";
    }
    public static bool operator !=(FullName left, FullName right) => $"{left.Name}{left.Surname}" != $"{right.Name}{right.Surname}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }
    #endregion
}