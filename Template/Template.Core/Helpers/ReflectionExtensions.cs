using System;
using System.Reflection;

namespace Template.Core
{
    public static class ReflectionExtensions
    {
        public static PropertyInfo GetNestedProperty(this Type type, object value, string dotSeparatedNotation)
        {
            var parts = dotSeparatedNotation.Split('.');

            if (parts.Length == 1)
                return type.GetProperty(dotSeparatedNotation);

            PropertyInfo property = null;

            for (int i = 0; i < parts.Length; i++)
            {
                property = type.GetProperty(parts[i]);
                var currentValue = property.GetValue(value);

                if (currentValue == null && i != parts.Length - 1)
                    property.SetValue(value, Activator.CreateInstance(property.PropertyType));

                value = property.GetValue(value);
                type = value?.GetType();
            }

            return property;
        }
    }
}
