using YahooFinance.NET;

namespace StockAnalysis.Managers;

public interface IStockData
{
    Task<List<YahooHistoricalPriceData>> GetStockData(string symbol, DateTime startDate, DateTime endDate);
}

public class StockData : IStockData
{
    public async Task<List<YahooHistoricalPriceData>> GetStockData(string symbol, DateTime startDate, DateTime endDate) =>
        await Task.Run(() => new YahooFinanceClient(string.Empty, string.Empty).GetDailyHistoricalPriceData(symbol, startDate, endDate));
}
