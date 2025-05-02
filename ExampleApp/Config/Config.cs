using EnvConfig;

namespace ExampleApp.Config
{
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
}