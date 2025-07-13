namespace WeatherDataApiTest.Models;

using Newtonsoft.Json;

public class ErrorResponse
{
    [JsonProperty("message")]
    public string? Message { get; set; }
}
