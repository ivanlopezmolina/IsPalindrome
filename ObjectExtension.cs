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


 var dictionary = person.ToDictionary();
