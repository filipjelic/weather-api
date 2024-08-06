using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Weather.GetWeatherByCity;
using Weather.Application.Weather.GetWeatherByDate;
using WeatherAPI.Controllers.Base;

namespace WeatherAPI.Controllers;

[Route("api/[controller]")]
public sealed class WeatherForecastController : BaseController
{
    public WeatherForecastController(ISender sender): base(sender)
    {
    }
    
    [HttpGet("city")]
    public async Task<IResult> GetWeatherByCity(string city, CancellationToken cancellationToken)
    {
        var query = new GetWeatherByCityQuery(city);

        var response = await Sender.Send(query, cancellationToken);
        
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response.Error);
    }
    
    /// <summary>
    /// Unfortunately this endpoint won't work since the API requires paid Developer subscription
    /// to have access to historical data.
    /// Keeping it as is if for reviewers.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>N/A</returns>
    [HttpGet("date")]
    [Obsolete("Requires Developer subscription")]
    public async Task<IResult> GetWeatherByDate(DateTime date,int latitude, int longitude, CancellationToken cancellationToken)
    {
        var query = new GetWeatherByDateQuery(date, latitude, longitude);

        var response = await Sender.Send(query, cancellationToken);
        
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response.Error);    
    }
}


