﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TechOnIt.Application.Common.Extentions;

public static class EnumExtensions
{
#nullable enable
    public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
    {
        Assert.NotNull(value, nameof(value));
        List<string> messages = new List<string>();

        var attribute = value.GetType().GetField(value.ToString())
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        if (attribute == null)
            return messages[0];
        object? propValue = attribute.GetType()
            .GetProperty(property.ToString())
            .GetValue(attribute, null);
        if (propValue == null)
            return messages[0];
        return propValue.ToString();
    }

    public static List<string> ToDisplays(this Enum value, DisplayProperty property = DisplayProperty.Name)
    {
        Assert.NotNull(value, nameof(value));
        List<string> Messages = new List<string>();

        var attribute = value.GetType().GetField(value.ToString())
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        if (attribute == null)
            return Messages;

        var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
        Messages.Add(propValue.ToString());
        return Messages;
    }
}

public enum DisplayProperty
{
    Description,
    GroupName,
    Name,
    Prompt,
    ShortName,
    Order
}