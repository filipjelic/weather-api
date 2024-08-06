using Microsoft.Extensions.Options;
using Weather.Application.Options;

namespace Weather.Application.OpenWeatherMapApi;

public class OpenWeatherMapApiClient
{
    private readonly HttpClient _httpClient;
    private readonly WeatherOptions _weatherOptions;
    
    public OpenWeatherMapApiClient(HttpClient httpClient, IOptions<WeatherOptions> weatherOptions)
    {
        _httpClient = httpClient;
        _weatherOptions = weatherOptions.Value;
    }
    
    public async Task<HttpResponseMessage> GetWeatherData(string city, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            ApiUrlProvider.FormatUriByLocation(city, _weatherOptions.ApiKey), cancellationToken);
        return response;
    }
    
    public async Task<HttpResponseMessage> GetWeatherData(DateTime date, int latitude, int longitude, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            ApiUrlProvider.FormatUriByDate(date, latitude, longitude, _weatherOptions.ApiKey), cancellationToken);
        return response;
    }
}

