using Microsoft.Extensions.Caching.Memory;
using YahooFinance.NET;

namespace StockAnalysis.Managers;

public interface IStockData
{
    Task<List<YahooHistoricalPriceData>> GetStockData(string symbol, DateTime? startDate, DateTime? endDate);
    Task<List<decimal>?> BacktestStock(BacktestInstructions instructions);
}

public class StockData : IStockData
{
    private IMemoryCache cache;

    public StockData(IMemoryCache cache)
    {
        this.cache = cache;
    }

    public async Task<List<YahooHistoricalPriceData>> GetStockData(string symbol, DateTime? startDate, DateTime? endDate) =>
        await cache.GetOrCreateAsync((symbol, startDate, endDate), async cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await Task.Run(() =>
                new YahooFinanceClient(string.Empty, string.Empty).GetDailyHistoricalPriceData(symbol, startDate, endDate));
        });
    

    public async Task<List<decimal>?> BacktestStock(BacktestInstructions instructions)
    {
        var stockData = await GetStockData(instructions.Ticker ?? string.Empty, instructions.StartDate, instructions.EndDate);
        
        if(stockData is null)
        {
            return null;
        }

        var result = new List<decimal>
        {
            stockData.First().Close * instructions.NumShares
        };

        foreach(var stock in stockData)
        {
            //var sell = instructions
        }

        return null;
    }

    private List<int> SimpleMovingAverage(int days, List<YahooHistoricalPriceData> data)
    {
        return null;
    }

}
