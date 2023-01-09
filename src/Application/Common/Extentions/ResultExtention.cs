using System.Collections;
using System.Text;
using System.Text.Json;

namespace TechOnIt.Application.Common.Extentions;

/// <summary>
/// for Reza : pls dont do not make any change on this class befor talk to me . (ashkan)
/// </summary>


public static class ResultExtention
{
    public static object IdResult(object parameter) => new { Id = parameter };
    public static object BooleanResult(bool parameter) => new { Succedded = parameter };
    public static object BooleanResult(bool? parameter) => new { Succedded = parameter };
    public static object ConcurrencyResult(object parameter) => new { Key = parameter };
    public static object GetJsonResult(object parameter)=>new {Result = JsonSerializer.Serialize(parameter) };
    public static object ListResult(object parameter) => new { List = parameter };
    public static object TokenResult(object parameter) => new {Token = parameter };
    public static object OtpResult(object parameter) => new {OtpCode = parameter };
    public static object NotFound(object parameter)
    {
        var type = new ManageObjects(parameter);
        StringBuilder errors = new StringBuilder();

        if (type.IsString) errors.Append(parameter.ToString());
        else if (type.IsArray)
        {
            foreach (var item in parameter as IEnumerable)
            {
                errors.Append($"{item.ToString()}-");
            }
        }
        else if (type.IsList)
        {
            foreach (var item in parameter as IEnumerable)
            {
                errors.Append($"{item.ToString()}-");
            }
        }

        return new { NotFound = errors.ToString() };
    }
    public static object Failed(object parameter)
    {
        var type = new ManageObjects(parameter);
        StringBuilder errors = new StringBuilder();

        if (type.IsString) errors.Append(parameter.ToString());
        else if (type.IsArray)
        {
            foreach (var item in parameter as IEnumerable)
            {
                errors.Append($"{item.ToString()}-");
            }
        }
        else if (type.IsList)
        {
            foreach (var item in parameter as IEnumerable)
            {
                errors.Append($"{item.ToString()}-");
            }
        }

        return new { Errors = errors.ToString() };
    }
}

public record ManageObjects
{
    public ManageObjects(object model)
    {
        ArgumentNullException.ThrowIfNull(model);

        IsList = IsObjectList(model);
        IsDictionary = IsObjectDictionary(model);
        IsString = IsObjectString(model);
        IsArray = IsObjectArray(model);
    }

    public bool IsDictionary { get; private set; }
    public bool IsList { get; private set; }
    public bool IsString { get; private set; }
    public bool IsArray { get; private set; }

    private bool IsObjectArray(object o)
    {
        return o.GetType().IsArray;
    }

    private bool IsObjectString(object o)
    {
        if (o == null) return false;
        return o.GetType().IsAssignableFrom(typeof(String)) || o.GetType().IsAssignableFrom(typeof(string));
    }

    private bool IsObjectList(object o)
    {
        if (o == null) return false;
        return o is IList &&
               o.GetType().IsGenericType &&
               o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
    }

    private bool IsObjectDictionary(object o)
    {
        if (o == null) return false;
        return o is IDictionary &&
               o.GetType().IsGenericType &&
               o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
    }
}