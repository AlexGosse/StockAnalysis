import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>High ($)</th>
            <th>Low ($)</th>
            <th>Close ($)</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.high}</td>
              <td>{forecast.low}</td>
              <td>{forecast.close}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Stock Data</h1>
        <p>Stock data</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('stockdata');
    console.log(response)
    const data = await response.json();
    console.log(data)
    this.setState({ forecasts: data, loading: false });
  }
}
