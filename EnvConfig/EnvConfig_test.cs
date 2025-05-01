namespace EnvConfig;

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

public class Tests_Int
{
    [Test]
    public void Test_int_no_set_so_use_default_value()
    {
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.RETRY_COUNT, Is.EqualTo(3));
    }

    [Test]
    public void Test_int_set_env_to_use()
    {
        Environment.SetEnvironmentVariable("RETRY_COUNT", "1234");
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.RETRY_COUNT, Is.EqualTo(1234));
    }
}

public class Tests_Double
{
    [Test]
    public void Test_double_no_set_so_use_default_value()
    {
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.MY_DOUBLE, Is.EqualTo(0.123).Within(0.0001));
    }

    [Test]
    public void Test_double_set_env_to_use()
    {
        Environment.SetEnvironmentVariable("MY_DOUBLE", "3.14");
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.MY_DOUBLE, Is.EqualTo(3.14).Within(0.0001));
    }
}

public class Tests_String
{

    [Test]
    public void Test_string_no_set_so_use_default_value()
    {
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.API_ENDPOINT, Is.EqualTo("https://default.api"));
    }

    [Test]
    public void Test_string_set_env_to_use()
    {
        // create a uuidV4 string to test
        var uuid = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable("API_ENDPOINT", uuid);
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.API_ENDPOINT, Is.EqualTo(uuid));
    }
}

public class Tests_Bool
{

    [Test]
    public void Test_bool_no_set_so_use_default_value()
    {
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.MY_BOOL, Is.EqualTo(true));
        Assert.That(config.MY_BOOL_2, Is.EqualTo(false));
    }

    [Test]
    public void Test_bool_set_values_to_use()
    {
        Environment.SetEnvironmentVariable("MY_BOOL", "false");
        Environment.SetEnvironmentVariable("MY_BOOL_2", "true");
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.MY_BOOL, Is.EqualTo(false));
        Assert.That(config.MY_BOOL_2, Is.EqualTo(true));
    }

    [Test]
    public void Test_bool_set_invalid_value_so_use_default()
    {
        Environment.SetEnvironmentVariable("MY_BOOL", "foo");
        Environment.SetEnvironmentVariable("MY_BOOL_2", "bar");
        var config = EnvConfig.Load<MyConfig>();
        Assert.That(config.MY_BOOL, Is.EqualTo(true), "MY_BOOL should be true because the env var is invalid");
        Assert.That(config.MY_BOOL_2, Is.EqualTo(false), "MY_BOOL_2 should be false because the env var is invalid");
    }
}