namespace WeatherDataApiTest.Configuration;

using Microsoft.Extensions.Configuration;

public class TestConfiguration
{
    private readonly IConfiguration _configuration;

    public TestConfiguration()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<TestConfiguration>()
            .Build();
    }

    public string BaseUrl => _configuration["WeatherApi:BaseUrl"] ?? throw new InvalidOperationException("BaseUrl not configured");
    public string ApiKey => _configuration["WeatherApi:ApiKey"] ?? throw new InvalidOperationException("ApiKey not configured");
}