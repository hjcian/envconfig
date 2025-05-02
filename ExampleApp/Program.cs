using EnvConfig;
using ExampleApp.Config; // Defined in the Config/Config.cs file

class Program
{
    static void Main(string[] args)
    {
        // Load the configuration from environment variables
        var config = EnvConfigLoader.Load<AppConfig>();

        // Use the loaded configuration
        Console.WriteLine($"應用名稱: {config.AppName}");
        Console.WriteLine($"最大重試次數: {config.MaxRetries}");
        Console.WriteLine($"啟用日誌: {config.EnableLogging}");
        Console.WriteLine($"逾時秒數: {config.TimeoutSeconds}");
    }
}