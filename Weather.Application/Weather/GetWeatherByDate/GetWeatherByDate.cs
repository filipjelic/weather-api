using Weather.Application.Abstraction;
using WeatherService.Application.Weather;

namespace Weather.Application.Weather.GetWeatherByDate;

public record GetWeatherByDateQuery(DateTime Date, int Latitude, int Longitude) : IQuery<OpenWeatherMapResponses.OpenWeatherMapSuccess>;