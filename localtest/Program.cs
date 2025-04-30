// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var envValue = "FalsE";
// var envValue = "true";

if (!string.IsNullOrEmpty(envValue) && bool.TryParse(envValue, out bool result))
{
    Console.WriteLine($"Parsed boolean value: {result}");
}
else
{
    Console.WriteLine($"envValue is null or empty: {envValue}");
}