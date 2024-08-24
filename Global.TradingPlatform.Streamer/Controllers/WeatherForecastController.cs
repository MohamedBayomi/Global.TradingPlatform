using Microsoft.AspNetCore.Mvc;

namespace Global.TradingPlatform.Streamer.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetSnapshotOrders")]
    public void GetSnapshotOrders()
    {
    }
}
