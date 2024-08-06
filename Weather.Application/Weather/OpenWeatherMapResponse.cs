using System.Text.Json.Serialization;

namespace WeatherService.Application.Weather;

public class OpenWeatherMapResponses
{
    public record OpenWeatherMapSuccess(
    [property: JsonPropertyName("coord")] Coord Coordinates,
    [property: JsonPropertyName("weather")] IReadOnlyList<Weather> Weather,
    [property: JsonPropertyName("base")] string Base,
    [property: JsonPropertyName("main")] Main Main,
    [property: JsonPropertyName("visibility")] int? Visibility,
    [property: JsonPropertyName("wind")] Wind Wind,
    [property: JsonPropertyName("rain")] Rain Rain,
    [property: JsonPropertyName("clouds")] Clouds Clouds,
    [property: JsonPropertyName("dt")] int? TimeOfDataCalculation,
    [property: JsonPropertyName("sys")] Sys Sys,
    [property: JsonPropertyName("timezone")] int? Timezone,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("cod")] int? Cod
);

public record Clouds(
    [property: JsonPropertyName("all")] int? All
);

public record Coord(
    [property: JsonPropertyName("lon")] double? Lon,
    [property: JsonPropertyName("lat")] double? Lat
);

public record Main(
    [property: JsonPropertyName("temp")] double? Temp,
    [property: JsonPropertyName("feels_like")] double? FeelsLike,
    [property: JsonPropertyName("temp_min")] double? TempMin,
    [property: JsonPropertyName("temp_max")] double? TempMax,
    [property: JsonPropertyName("pressure")] int? Pressure,
    [property: JsonPropertyName("humidity")] int? Humidity,
    [property: JsonPropertyName("sea_level")] int? SeaLevel,
    [property: JsonPropertyName("grnd_level")] int? GrndLevel
);

public record Rain(
    [property: JsonPropertyName("1h")] double? _1h
);

public record Sys(
    [property: JsonPropertyName("type")] int? Type,
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("sunrise")] int? Sunrise,
    [property: JsonPropertyName("sunset")] int? Sunset
);

public record Weather(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("main")] string Main,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("icon")] string Icon
);

public record Wind(
    [property: JsonPropertyName("speed")] double? Speed,
    [property: JsonPropertyName("deg")] int? Direction,
    [property: JsonPropertyName("gust")] double? Gust
);


public record OpenWeatherMapError(
    [property: JsonPropertyName("cod")] string Code,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("parameters")] IReadOnlyList<string> Parameters
);
}
