using YahooFinance.NET;

namespace StockAnalysis.Managers;

public interface IStockData
{
    void GetStockData(string symbol1, DateTime startDate, DateTime endDate);
}

public class StockData : IStockData
{
    public void GetStockData(string symbol, DateTime startDate, DateTime endDate)
    {
        var yahooPriceHistory = new YahooFinanceClient("B=YYYYYY", "XXXXXXX ").GetDailyHistoricalPriceData(symbol, startDate, endDate);
    }
}

