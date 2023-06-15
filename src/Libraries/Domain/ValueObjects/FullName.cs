using System;
using System.Collections.Generic;
using TechOnIt.Domain.Common;

namespace TechOnIt.Domain.ValueObjects;

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
    public void SetFullName(string name, string surname)
    {
        if (name is null || surname is null)
            throw new ArgumentNullException($"name or surname can not be null in this case");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException($"the parameter name can not be null or empty");

        Name = name;
        Surname = surname;
    }

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