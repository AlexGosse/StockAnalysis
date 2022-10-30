using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockAnalysis.Managers;

public record BacktestInstructions
(
    IInstruction?[] BuyWhen,
    IInstruction?[] SellWhen,
    DateTime? StartDate,
    DateTime? EndDate,
    string Ticker = "NYSE",
    int NumShares = 100
);

public interface IInstruction { };
public record SimpleMovingAverage(int days, bool isAbove, bool? isAnd) : IInstruction;
public record ClosePrice(decimal price, bool isAbove, bool? isAnd) : IInstruction;
public record Drawdown(decimal drawdown, bool isAbove, bool? isAnd) : IInstruction;

public enum InstructionType
{
    SimpleMovingAverage,
    ClosePrice,
    Drawdown
}