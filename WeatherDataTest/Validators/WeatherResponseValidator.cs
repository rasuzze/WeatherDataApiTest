namespace WeatherDataApiTest.Helpers;

using FluentAssertions;
using Models;
using RestSharp;
using System.Net;

public static class WeatherResponseValidator
    {
        public static void ValidateSuccess(RestResponse? response)
        {
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(HttpStatusCode.OK);
            response?.IsSuccessful.Should().BeTrue();
        }

        public static void ValidateContainsWeatherData(WeatherResponse? weatherResponse)
        {
            weatherResponse?.Main.Should().NotBeNull();
            weatherResponse?.Code.Should().Be(200);
            weatherResponse?.Coord.Should().NotBeNull();
            weatherResponse?.Weather.Should().NotBeNullOrEmpty();
            weatherResponse?.Weather?.Count.Should().BeGreaterThan(0);
        }

        public static void ValidateErrorMessage(ErrorResponse? errorResponse)
        {
            errorResponse.Should().NotBeNull();
            errorResponse?.Message.Should().NotBeNullOrEmpty();
        }

        public static void ValidateCityName(WeatherResponse? weatherResponse, string expectedCityName)
        {
            weatherResponse?.Name.Should().Be(expectedCityName);
        }

        public static void ValidateCoordinates(WeatherResponse? weatherResponse, double expectedLat, double expectedLon)
        {
            weatherResponse?.Coord.Should().NotBeNull();
            weatherResponse?.Coord?.Lat.Should().BeApproximately(expectedLat, 0.1);
            weatherResponse?.Coord?.Lon.Should().BeApproximately(expectedLon, 0.1);
            weatherResponse?.Code.Should().Be(200);
        }

        public static void ValidateTemperatureUnit(WeatherResponse? weatherResponse, string units)
        {
            var temperature = weatherResponse?.Main?.Temp;
            switch (units.ToLower())
            {
                case "metric":
                    temperature.Should().BeInRange(-100, 100);
                    break;
                case "imperial":
                    temperature.Should().BeInRange(-200, 200);
                    break;
                case "standard":
                    temperature.Should().BeInRange(0, 500);
                    break;
            }
        }
    }