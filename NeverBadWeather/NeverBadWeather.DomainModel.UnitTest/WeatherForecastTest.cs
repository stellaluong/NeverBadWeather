using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NeverBadWeather.DomainModel.UnitTest
{
    class WeatherForecastTest
    {
        [Test]
        public void TestGetStats()
        {
            var tempForecast = new TemperatureForecast(30, DateTime.Now, DateTime.MaxValue);
            var forecast =  new WeatherForecast(new []{tempForecast});
            var temperatureStatistics = forecast.GetStats();
            Assert.AreEqual(30, temperatureStatistics.Max);
            Assert.AreEqual(tempForecast, forecast.Temperatures[0]);
        }

        [Test]
        public void TestWeatherForecastOK()
        {
            var tempForecast = new TemperatureForecast(30,DateTime.Now, DateTime.MaxValue);
            var forecast = new WeatherForecast(new []{tempForecast});
            Assert.AreEqual(tempForecast.ToString(), forecast.ToString());
        }


    }
}
