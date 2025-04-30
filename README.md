- [EnvConfig](#envconfig)
    - [Installation](#installation)
    - [Usage](#usage)
    - [Features](#features)
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
    public string? ApiEndpoint { get; set; }

    [EnvVar("RETRY_COUNT", 3)]
    public double RetryCount { get; set; }
}

public class Program
{
    public static void Main()
    {
        var config = EnvConfig.Load<MyConfig>();
        Console.WriteLine($"API_ENDPOINT: {config.ApiEndpoint}");
        Console.WriteLine($"RETRY_COUNT: {config.RetryCount}");
    }
}
```
## Features
- String defaults: Provide a default string value.
- Boolean defaults: Use true or false for booleans.
- Numeric defaults: Supply a numeric default, like double.
- Override with environment variables: If set, the environment value overrides the default.

## Contributing
Feel free to open pull requests or issues to improve the library. You’re welcome to suggest enhancements, report bugs, or contribute documentation.