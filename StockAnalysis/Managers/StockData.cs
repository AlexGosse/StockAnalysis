using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using YahooFinance.NET;

namespace StockAnalysis.Managers;

public interface IStockData
{
    Task<List<YahooHistoricalPriceData>> GetStockData(string symbol, DateTime? startDate, DateTime? endDate);
    Task<List<decimal>?> BacktestStock(string instructions);
}

public class StockData : IStockData
{
    private readonly IMemoryCache cache;
    private JsonSerializerSettings jsonSerializerSettings;

    public StockData(IMemoryCache cache)
    {
        this.cache = cache;
        jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    }

    public async Task<List<YahooHistoricalPriceData>> GetStockData(string symbol, DateTime? startDate, DateTime? endDate)
    {
        startDate ??= DateTime.Today.AddYears(-20);
        endDate ??= DateTime.Today;
        return await cache.GetOrCreateAsync((symbol, startDate, endDate), async cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await Task.Run(() =>
                new YahooFinanceClient(string.Empty, string.Empty).GetDailyHistoricalPriceData(symbol, startDate, endDate));
        });
    }

    public async Task<List<decimal>?> BacktestStock(string instructionString)
    {
        var instructions = JsonConvert.DeserializeObject<BacktestInstructions>(instructionString, jsonSerializerSettings);

        if(instructions is null)
        {
            return null;
        }
        
        var stockData = await GetStockData(instructions?.Ticker, instructions.StartDate, instructions.EndDate);

        if (stockData is null)
        {
            return null;
        }

        var result = new List<decimal>
        {
            stockData.First().Close * instructions.NumShares
        };

        foreach (var stock in stockData)
        {
            //var sell = instructions
        }

        return null;
    }

    private decimal SimpleMovingAverage(DateTime startDate, DateTime endDate, List<YahooHistoricalPriceData> data) =>
        data.Where(x => startDate <= x.Date && x.Date <= endDate).Average(x => x.Close);
}
