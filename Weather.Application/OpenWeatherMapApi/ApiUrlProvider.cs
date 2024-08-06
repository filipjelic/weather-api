namespace Weather.Application.OpenWeatherMapApi;

public static class ApiUrlProvider
{
    public static string FormatUriByLocation(string city, string apiKey) => 
        $"2.5/weather?q={city}&appid={apiKey}";
    public static string FormatUriByDate(DateTime date, int latitude, int longitude, string apiKey) => 
        $"3.0/onecall/day_summary?lat={latitude}&lon={longitude}date={date:yyyy-mm-dd}&appid={apiKey}";
}