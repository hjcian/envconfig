
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
}


public class EnvConfig
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
                    if (string.IsNullOrEmpty(envValue))
                    {
                        envValue = attr.DefaultDoubleValue.ToString();
                    }
                    prop.SetValue(instance, double.Parse(envValue));
                }
                else
                {
                    if (string.IsNullOrEmpty(envValue))
                    {
                        envValue = attr.DefaultStringValue;
                    }
                    prop.SetValue(instance, envValue);
                }
            }
        }

        return instance;
    }
}

public class MyEnvConfig
{
    [EnvVar("GRPC_PORT", "6300")]
    public string? GRPC_PORT { get; set; }

    [EnvVar("IMS_HOST", "inventory-management-service")]
    public string? IMS_HOST { get; set; }

    [EnvVar("COOL_FEATURE_ENABLED", true)]
    public bool COOL_FEATURE_ENABLED { get; set; }
}


public class Program
{
    public static void Main(string[] args)
    {
        var config = EnvConfig.Load<MyEnvConfig>();
        Console.WriteLine($"GRPC_PORT value: {config.GRPC_PORT}");
        Console.WriteLine($"IMS_HOST value: {config.IMS_HOST}");
        Console.WriteLine($"COOL_FEATURE_ENABLED value: {config.COOL_FEATURE_ENABLED}");
    }
}