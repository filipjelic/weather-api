using System.ComponentModel.DataAnnotations;

namespace Weather.Application.Options;

public class WeatherOptions
{
    public const string Key = "WeatherApi";

    [Required] public string BaseUrl { get; set; } = string.Empty;

    [Required] public string ApiKey { get; set; } = string.Empty;
}