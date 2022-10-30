import React, { Component } from "react";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.fetchStockData();
    this.fetchBacktestData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>High ($)</th>
            <th>Low ($)</th>
            <th>Close ($)</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.high}</td>
              <td>{forecast.low}</td>
              <td>{forecast.close}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchData.renderForecastsTable(this.state.forecasts)
    );

    return (
      <div>
        <h1 id="tabelLabel">Stock Data</h1>
        <p>Stock data</p>
        {contents}
      </div>
    );
  }

  async fetchStockData() {
    const response = await fetch("stockdata?symbol=AAPL");
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }

  async fetchBacktestData() {
    let backtestInstructions = `{
      "$type": "StockAnalysis.Managers.BacktestInstructions, StockAnalysis",
      "BuyWhen": [{
        "$type": "StockAnalysis.Managers.SimpleMovingAverage, StockAnalysis",
        "days": 4,
        "isAbove": true,
        "isAnd": false
      },
      {
        "$type": "StockAnalysis.Managers.ClosePrice, StockAnalysis",
        "price": 4.1,
        "isAbove": true,
        "isAnd": false
      }
      ],
      "StartDate": null,
      "EndDate": null,
      "Ticker": "NYSE",
      "NumShares": 100
    }`;

    const response = await fetch("backtest", {
      headers: {
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify(backtestInstructions),
    });
  }
}
