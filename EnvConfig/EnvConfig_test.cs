namespace EnvConfig;

public class TestEnvConfig
{
    [EnvVar("MY_STRING", "DEFAULT_STRING_FOR_TEST")]
    public string? MY_STRING { get; set; }

    [EnvVar("MY_BOOL", true)]
    public bool? MY_BOOL { get; set; }

    [EnvVar("MY_BOOL_2", false)]
    public bool? MY_BOOL_2 { get; set; }
}

public class Tests
{

    [Test]
    public void Test_string_no_set_so_use_default_value()
    {
        var config = EnvConfig.Load<TestEnvConfig>();
        Assert.That(config.MY_STRING, Is.EqualTo("DEFAULT_STRING_FOR_TEST"));
    }

    [Test]
    public void Test_string_set_env_to_use()
    {
        // create a uuidv4 string to test
        var uuid = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable("MY_STRING", uuid);
        var config = EnvConfig.Load<TestEnvConfig>();
        Assert.That(config.MY_STRING, Is.EqualTo(uuid));
    }

    [Test]
    public void Test_bool_no_set_so_use_default_value()
    {
        var config = EnvConfig.Load<TestEnvConfig>();
        Assert.That(config.MY_BOOL, Is.EqualTo(true));
        Assert.That(config.MY_BOOL_2, Is.EqualTo(false));
    }

    [Test]
    public void Test_bool_set_values_to_use()
    {
        Environment.SetEnvironmentVariable("MY_BOOL", "false");
        Environment.SetEnvironmentVariable("MY_BOOL_2", "true");
        var config = EnvConfig.Load<TestEnvConfig>();
        Assert.That(config.MY_BOOL, Is.EqualTo(false));
        Assert.That(config.MY_BOOL_2, Is.EqualTo(true));
    }

    // [Test]
    // public void Test_register_non_existent_env_key()
    // {
    //     Assert.Pass();
    // }
}