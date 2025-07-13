namespace WeatherDataApiTest.Services;

using RestSharp;
using Configuration;

public class WeatherApiService
{
    private readonly RestClient _client;
    private readonly TestConfiguration _config;

    public WeatherApiService()
    {
        _config = new TestConfiguration();
        _client = new RestClient(_config.BaseUrl);
    }

    public async Task<RestResponse> GetWeatherByCity(string cityName, string units = "metric", string? apiKey = null)
    {
        var request = new RestRequest("weather", Method.Get);
        request.AddParameter("q", cityName);
        request.AddParameter("units", units);
        request.AddParameter("appid", apiKey ?? _config.ApiKey);

        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetWeatherByCoordinates(double lat, double lon, string units = "metric", string? apiKey = null)
    {
        var request = new RestRequest("weather", Method.Get);
        request.AddParameter("lat", lat.ToString());
        request.AddParameter("lon", lon.ToString());
        request.AddParameter("units", units);
        request.AddParameter("appid", apiKey ?? _config.ApiKey);

        return await _client.ExecuteAsync(request);
    }
}