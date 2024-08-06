using System.Net;
using System.Web;
using Microsoft.Extensions.Caching.Distributed;

namespace Weather.Cache;

/// <summary>
/// A good starting point to cache responses
/// </summary>
public class CacheWeatherHandler : DelegatingHandler
{
    private readonly IDistributedCache _cache;

    private static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
    };
    
    public CacheWeatherHandler(IDistributedCache cache)
    {
        _cache = cache;
    }


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var queryString = HttpUtility.ParseQueryString(request.RequestUri!.Query);
        var key = queryString["q"];
        var cached = await _cache.GetStringAsync(key, token: cancellationToken);

        if (cached is not null)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(cached)
            };
        }
        
        var response = await base.SendAsync(request, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            await _cache.SetStringAsync(key, 
                content,
                DefaultExpiration, 
                cancellationToken);
        }
    
        return response;
    }
}