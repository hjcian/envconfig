using EnvConfig;

// 定義配置類別
public class AppConfig
{
    [EnvVar("APP_NAME", "ExampleApp")]
    public string? AppName { get; set; }

    [EnvVar("MAX_RETRIES", 3)]
    public int? MaxRetries { get; set; }

    [EnvVar("ENABLE_LOGGING", true)]
    public bool? EnableLogging { get; set; }

    [EnvVar("TIMEOUT_SECONDS", 30.5)]
    public double? TimeoutSeconds { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // 載入環境變數配置
        var config = EnvConfigLoader.Load<AppConfig>();

        // 使用配置值
        Console.WriteLine($"應用名稱: {config.AppName}");
        Console.WriteLine($"最大重試次數: {config.MaxRetries}");
        Console.WriteLine($"啟用日誌: {config.EnableLogging}");
        Console.WriteLine($"逾時秒數: {config.TimeoutSeconds}");
    }
}