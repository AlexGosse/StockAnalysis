using Microsoft.AspNetCore.Mvc;
using StockAnalysis.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStockData, StockData>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapGet("/stockdata",
    async ([FromServices] IStockData stockData, [FromQuery] string symbol, DateTime? start, DateTime? end) =>
    await stockData.GetStockData(symbol, start, end));

app.MapPost("/backtest",
    async ([FromServices] IStockData stockData, [FromBody] string backtestInstructions) =>
    await stockData.BacktestStock(backtestInstructions));

app.Run();
