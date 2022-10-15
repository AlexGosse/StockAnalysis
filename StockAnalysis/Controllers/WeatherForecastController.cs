using Microsoft.AspNetCore.Mvc;
using StockAnalysis.Managers;
using YahooFinanceApi;

namespace StockAnalysis.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IStockData _stockData;


    public WeatherForecastController(ILogger<WeatherForecastController> logger, IStockData stockData)
    {
        _logger = logger;
        _stockData = stockData;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        _stockData.GetStockData("AAPL", DateTime.Today.AddMonths(-15), DateTime.Today);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

