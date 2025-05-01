
namespace EnvConfig;
using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Property)]
public sealed class EnvVarAttribute : Attribute
{
    public string Key { get; }
    public string? DefaultStringValue { get; }
    public bool? DefaultBoolValue { get; }

    public double? DefaultDoubleValue { get; }
    public int? DefaultIntValue { get; }

    public EnvVarAttribute(string key, string defaultValue)
    {
        Key = key;
        DefaultStringValue = defaultValue;
    }

    public EnvVarAttribute(string key, bool defaultValue)
    {
        Key = key;
        DefaultBoolValue = defaultValue;
    }

    public EnvVarAttribute(string key, double defaultValue)
    {
        Key = key;
        DefaultDoubleValue = defaultValue;
    }
    public EnvVarAttribute(string key, int defaultValue)
    {
        Key = key;
        DefaultIntValue = defaultValue;
    }
}


public class EnvConfigLoader
{
    public static T Load<T>() where T : new()
    {
        var instance = new T();
        var props = typeof(T).GetProperties();
        foreach (var prop in props)
        {
            var attr = prop.GetCustomAttribute<EnvVarAttribute>();
            if (attr != null)
            {
                var envValue = Environment.GetEnvironmentVariable(attr.Key);
                if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                {
                    var boolValue = attr.DefaultBoolValue;
                    if (!string.IsNullOrEmpty(envValue) && bool.TryParse(envValue, out bool result))
                    {
                        // Only accept "true" or "false" (case insensitive) to be parsed as bool
                        // Otherwise, use the default value
                        boolValue = result;
                    }
                    prop.SetValue(instance, boolValue);
                }
                else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                {
                    var numericValue = attr.DefaultDoubleValue;
                    if (!string.IsNullOrEmpty(envValue) && double.TryParse(envValue, out double result))
                    {
                        numericValue = result;
                    }
                    prop.SetValue(instance, numericValue);
                }
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    var intValue = attr.DefaultIntValue;
                    if (!string.IsNullOrEmpty(envValue) && int.TryParse(envValue, out int result))
                    {
                        intValue = result;
                    }
                    prop.SetValue(instance, intValue);
                }
                else
                {
                    // Finally, assume it's a string property
                    var stringValue = attr.DefaultStringValue;
                    if (!string.IsNullOrEmpty(envValue))
                    {
                        stringValue = envValue;
                    }
                    prop.SetValue(instance, stringValue);
                }
            }
        }
        return instance;
    }
}