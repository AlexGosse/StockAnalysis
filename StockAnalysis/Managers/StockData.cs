using System;
using YahooFinanceApi;

namespace StockAnalysis.Managers;

public interface IStockData
{
    Task<(IReadOnlyList<Candle>?, string?)> GetStockData(string symbol, DateTime startDate, DateTime endDate);
}

public class StockData : IStockData
{
    public async Task<(IReadOnlyList<Candle>?, string?)> GetStockData(string symbol, DateTime startDate, DateTime endDate)
    {
        try
        {
            //var historicalDataTask = await Yahoo.GetHistoricalAsync(symbol);
            var history = await Yahoo.GetHistoricalAsync("AAPL", new DateTime(2016, 1, 1), new DateTime(2016, 7, 1), Period.Daily);
            var security = await Yahoo.Symbols(symbol).Fields(Field.LongName).QueryAsync();


            var ticker = security.GetValueOrDefault(symbol);
            var companyName = ticker?.LongName;

            //var historicalStockData = await historicalDataTask;

            return (history, companyName);
        }
        catch
        {
            return (null, null);
        }

    }
}

