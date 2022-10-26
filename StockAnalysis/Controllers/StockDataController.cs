using Microsoft.AspNetCore.Mvc;
using StockAnalysis.Managers;
using YahooFinance.NET;

namespace StockAnalysis.Controllers;

[ApiController]
[Route("[controller]")]
public class StockDataController : ControllerBase
{
    private readonly IStockData _stockData;

    // Simple Moving Average

    // Close Price

    // Drawdown

    //Is Above

    //Is Below

    public StockDataController(ILogger<StockDataController> logger, IStockData stockData)
    {
        _stockData = stockData;
    }

    [HttpGet("{symbol}")]
    public async Task<IEnumerable<YahooHistoricalPriceData>> Get(string symbol)
    {
        return await _stockData.GetStockData(symbol, DateTime.Today.AddMonths(-15), DateTime.Today);
    }

    [HttpPost]
    public async Task<IEnumerable<decimal>> Post([FromBody]BacktestInstructions instructions)
    {
        return null;
    }
}

