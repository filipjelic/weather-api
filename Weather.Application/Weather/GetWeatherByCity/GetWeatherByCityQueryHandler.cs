using System.Text.Json;
using Weather.Application.Abstraction;
using Weather.Application.OpenWeatherMapApi;
using WeatherService.Application.Weather;

namespace Weather.Application.Weather.GetWeatherByCity;

public class GetWeatherByCityQueryHandler : IQueryHandler<GetWeatherByCityQuery, OpenWeatherMapResponses.OpenWeatherMapSuccess>
{
    private readonly OpenWeatherMapApiClient _openWeatherMapApiClient;
    
    public GetWeatherByCityQueryHandler(OpenWeatherMapApiClient openWeatherMapApiClient)
    {
        _openWeatherMapApiClient = openWeatherMapApiClient;
    }
    
    public async Task<Result<OpenWeatherMapResponses.OpenWeatherMapSuccess>> Handle(GetWeatherByCityQuery request, CancellationToken cancellationToken)
    {
        var response = await _openWeatherMapApiClient.GetWeatherData(request.CityName, cancellationToken);

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<OpenWeatherMapResponses.OpenWeatherMapSuccess>(json);
        }

        var errorResponse = JsonSerializer.Deserialize<OpenWeatherMapResponses.OpenWeatherMapError>(json);
        
        return Result.Failure<OpenWeatherMapResponses.OpenWeatherMapSuccess>(new Error("OpenWeatherMap.Error", errorResponse.Message));
        
    }
}