using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;
using Weather.Application.OpenWeatherMapApi;
using Weather.Application.Options;
using Weather.Cache;

namespace WeatherAPI.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adding basic Rate Limiter (per IP) just to protect from too many API calls
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/></param>
    public static void RegisterRateLimiter(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy("fixed-by-ip", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromSeconds(10)
                    }));
        });
    }
    
    /// <summary>
    /// Registering Open Weather Map API client (as typed client)
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/></param>
    public static void RegisterHttpClient(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CacheWeatherHandler>();
        
        serviceCollection.AddHttpClient<OpenWeatherMapApiClient>((serviceProvider, client) =>
            {
                var settings = serviceProvider
                    .GetRequiredService<IOptions<WeatherOptions>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
            })  
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15)
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
            .AddHttpMessageHandler<CacheWeatherHandler>();
    }
    
}