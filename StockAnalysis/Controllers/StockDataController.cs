using Microsoft.AspNetCore.Mvc;
using StockAnalysis.Managers;
using YahooFinance.NET;

namespace StockAnalysis.Controllers;

[ApiController]
[Route("[controller]")]
public class StockDataController : ControllerBase
{
    private readonly IStockData _stockData;


    public StockDataController(ILogger<StockDataController> logger, IStockData stockData)
    {
        _stockData = stockData;
    }

    [HttpGet]
    public async Task<IEnumerable<YahooHistoricalPriceData>> Get()
    {
        return await _stockData.GetStockData("AAPL", DateTime.Today.AddMonths(-15), DateTime.Today);
    }
}

