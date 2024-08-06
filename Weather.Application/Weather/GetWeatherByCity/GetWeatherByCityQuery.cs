using Weather.Application.Abstraction;
using WeatherService.Application.Weather;

namespace Weather.Application.Weather.GetWeatherByCity;

public record GetWeatherByCityQuery(string CityName) : IQuery<OpenWeatherMapResponses.OpenWeatherMapSuccess>;