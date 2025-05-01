- [EnvConfig](#envconfig)
    - [Installation](#installation)
    - [Usage](#usage)
    - [TODO](#todo)
    - [Contributing](#contributing)


# EnvConfig

**EnvConfig** is a lightweight library that simplifies retrieving configuration values from environment variables. It uses attributes to map environment variables onto class properties, offering convenient defaults for each property type.

## Installation

```bash
dotnet add package EnvConfig
```

## Usage
1. Create a configuration class with properties annotated using [EnvVar].
2. Use EnvConfig.Load<YourConfig>() to populate those properties from environment variables if available; otherwise default values are used.

Example:

```csharp
public class MyConfig
{
    [EnvVar("API_ENDPOINT", "https://default.api")]
    public string? API_ENDPOINT { get; set; }

    [EnvVar("RETRY_COUNT", 3)]
    public int? RETRY_COUNT { get; set; }

    [EnvVar("MY_BOOL", true)]
    public bool? MY_BOOL { get; set; }

    [EnvVar("MY_BOOL_2", false)]
    public bool? MY_BOOL_2 { get; set; }

    [EnvVar("MY_DOUBLE", 0.123)]
    public double? MY_DOUBLE { get; set; }
}


public class Program
{
    public static void Main()
    {
        var config = EnvConfig.Load<MyConfig>();
        Console.WriteLine($"API_ENDPOINT: {config.API_ENDPOINT}");
        Console.WriteLine($"RETRY_COUNT: {config.RETRY_COUNT}");
    }
}
```

## TODO
- [ ] Publish to NuGet


## Contributing
Feel free to open pull requests or issues to improve the library. You’re welcome to suggest enhancements, report bugs, or contribute documentation.