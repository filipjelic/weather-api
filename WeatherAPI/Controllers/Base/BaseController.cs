using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WeatherAPI.Controllers.Base;

[EnableRateLimiting("fixed-by-ip")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;

    protected BaseController(ISender sender)
    {
        Sender = sender;
    }
}