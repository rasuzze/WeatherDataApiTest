namespace WeatherDataApiTest.Models;

using Newtonsoft.Json;

public class MainWeatherData
{
    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }
}