namespace WeatherDataApiTest.Models;

using Newtonsoft.Json;

public class WeatherResponse
{
    [JsonProperty("coord")]
    public Coordinates? Coord { get; set; }

    [JsonProperty("weather")]
    public List<Weather>? Weather { get; set; }

    [JsonProperty("main")]
    public MainWeatherData? Main { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("cod")]
    public int Code { get; set; }
}