namespace WeatherDataApiTest.Services;

using Newtonsoft.Json;
using Models;

public class WeatherResponseDeserializer
{
    private readonly JsonSerializerSettings _jsonSettings;

    public WeatherResponseDeserializer()
    {
        _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DateParseHandling = DateParseHandling.None
        };
    }

    public WeatherResponse? DeserializeWeatherResponse(string content)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            return JsonConvert.DeserializeObject<WeatherResponse>(content, _jsonSettings);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to deserialize weather response: {ex.Message}");
            return null;
        }
    }

    public ErrorResponse? DeserializeErrorResponse(string content)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            return JsonConvert.DeserializeObject<ErrorResponse>(content, _jsonSettings);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to deserialize error response: {ex.Message}");
            return null;
        }
    }
}