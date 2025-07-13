namespace WeatherDataApiTest.Models;

using Newtonsoft.Json;

public class Coordinates
{
    [JsonProperty("lon")]
    public double Lon { get; set; }

    [JsonProperty("lat")]
    public double Lat { get; set; }
}