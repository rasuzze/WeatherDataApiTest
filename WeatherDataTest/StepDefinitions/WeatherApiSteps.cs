namespace WeatherDataApiTest.StepDefinitions;

using FluentAssertions;
using Helpers;
using Reqnroll;
using RestSharp;
using Models;
using Services;
using System.Net;

[Binding]
public class WeatherApiSteps
{
    private readonly WeatherApiService _weatherApiService;
    private readonly WeatherResponseDeserializer _deserializer;
    private readonly ScenarioContext _scenarioContext;

    public WeatherApiSteps(ScenarioContext scenarioContext)
    {
        _weatherApiService = new WeatherApiService();
        _deserializer = new WeatherResponseDeserializer();
        _scenarioContext = scenarioContext;
    }

    [Given(@"I have access to the OpenWeatherMap API")]
    public void GivenIHaveAccessToTheOpenWeatherMapApi()
    {
        _weatherApiService.Should().NotBeNull();
    }

    [Given(@"I have a (.*) API key")]
    public void GivenIHaveAnApiKey(string apiKeyType)
    {
        string apiKey = apiKeyType.ToLower() switch
        {
            "valid" => string.Empty,
            "invalid" => "invalid_api_key_123",
            _ => throw new ArgumentException("Unsupported API key type.")
        };

        _scenarioContext["apiKey"] = apiKey;
    }

    [When(@"I request weather for (.*)")]
    public async Task WhenIRequestWeatherForCity(string cityName)
    {
        var apiKey = _scenarioContext.TryGetValue("apiKey", out var value) ? value?.ToString() : null;
        var response = await _weatherApiService.GetWeatherByCity(cityName, "metric", string.IsNullOrEmpty(apiKey) ? null : apiKey);
        _scenarioContext["response"] = response;
    }

    [When(@"I request weather with coordinates lat (.*) and lon (.*)")]
    public async Task WhenIRequestWeatherWithCoordinates(string lat, string lon)
    {
        double? latitude = double.TryParse(lat, out var latVal) ? latVal : null;
        double? longitude = double.TryParse(lon, out var lonVal) ? lonVal : null;

        var apiKey = _scenarioContext.TryGetValue("apiKey", out var value) ? value?.ToString() : null;

        var response = await _weatherApiService.GetWeatherByCoordinates(
            latitude ?? double.NaN,
            longitude ?? double.NaN,
            "metric",
            string.IsNullOrEmpty(apiKey) ? null : apiKey);

        _scenarioContext["response"] = response;
    }


    [When(@"I request the weather for (.*) in (.*) units")]
    public async Task WhenIRequestWeatherInUnits(string cityName, string units)
    {
        var apiKey = _scenarioContext.TryGetValue("apiKey", out var value) ? value?.ToString() : null;
        var response = await _weatherApiService.GetWeatherByCity(cityName, units, string.IsNullOrEmpty(apiKey) ? null : apiKey);
        _scenarioContext["response"] = response;
    }

    [Then(@"I should get a (.*) response")]
    public void ThenIShouldGetAResponse(string status)
    {
        var response = _scenarioContext["response"] as RestResponse;
        response.Should().NotBeNull();

        switch (status.ToLower())
        {
            case "successful":
                WeatherResponseValidator.ValidateSuccess(response);
                break;
            case "unauthorized":
                response?.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
                response?.IsSuccessful.Should().BeFalse();
                break;
            case "not found":
                response?.StatusCode.Should().Be(HttpStatusCode.NotFound);
                break;
            case "bad request":
                response?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response?.IsSuccessful.Should().BeFalse();
                break;
            default:
                throw new ArgumentException($"Unsupported status: {status}");
        }
    }

    [Then(@"the response should contain weather data")]
    public void ThenTheResponseShouldContainWeatherData()
    {
        var response = _scenarioContext["response"] as RestResponse;
        WeatherResponseValidator.ValidateSuccess(response);

        var weatherResponse = _deserializer.DeserializeWeatherResponse(response?.Content!);
        _scenarioContext["weatherResponse"] = weatherResponse;

        WeatherResponseValidator.ValidateContainsWeatherData(weatherResponse);
    }

    [Then(@"the response should contain error message")]
    public void ThenTheResponseShouldContainErrorMessage()
    {
        var response = _scenarioContext["response"] as RestResponse;
        var errorResponse = _deserializer.DeserializeErrorResponse(response?.Content!);
        _scenarioContext["errorResponse"] = errorResponse;

        WeatherResponseValidator.ValidateErrorMessage(errorResponse);
    }

    [Then(@"the city name should be (.*)")]
    public void ThenTheCityNameShouldBeValid(string expectedCityName)
    {
        var weatherResponse = _scenarioContext["weatherResponse"] as WeatherResponse;
        WeatherResponseValidator.ValidateCityName(weatherResponse, expectedCityName);
    }

    [Then(@"the coordinates should be approximately lat (.*) and lon (.*)")]
    public void ThenTheCoordinatesShouldBeApproximatelyLatAndLon(string lat, string lon)
    {
        var weatherResponse = _scenarioContext["weatherResponse"] as WeatherResponse;
        var expectedLat = double.Parse(lat);
        var expectedLon = double.Parse(lon);

        WeatherResponseValidator.ValidateCoordinates(weatherResponse, expectedLat, expectedLon);
    }

    [Then(@"the temperature should be in the correct unit for (.*)")]
    public void ThenTheTemperatureShouldBeInTheCorrectUnit(string units)
    {
        var weatherResponse = _scenarioContext["weatherResponse"] as WeatherResponse;
        WeatherResponseValidator.ValidateTemperatureUnit(weatherResponse, units);
    }
}
