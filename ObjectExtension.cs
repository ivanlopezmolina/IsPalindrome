public static class ObjectExtensions
{
    public static Dictionary<string, object> ToDictionary(this object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var dictionary = new Dictionary<string, object>();
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            dictionary.Add(property.Name, value);
        }

        return dictionary;
    }
}


using System;
using System.Collections.Generic;
using System.Reflection;

public static class ObjectExtensions
{
    public static Dictionary<string, string> ToDictionary(this object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var dictionary = new Dictionary<string, string>();
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            string formattedValue;

            if (value is string stringValue)
            {
                // Wrap strings in single quotes
                formattedValue = $"'{stringValue}'";
            }
            else
            {
                // Convert other types to string
                formattedValue = value?.ToString() ?? "null";
            }

            dictionary.Add(property.Name, formattedValue);
        }

        return dictionary;
    }
}


 var dictionary = person.ToDictionary();
