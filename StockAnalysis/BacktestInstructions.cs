namespace StockAnalysis;

public record BacktestInstructions
(
    DateTime? StartDate,
    DateTime? EndDate,
    string Ticker = "NYSE",
    int NumShares = 100
)
{
    readonly Dictionary<string, string> backtestInfo = new()
    {
        { "SimpleMovingAverage", "test" }
    };
}

