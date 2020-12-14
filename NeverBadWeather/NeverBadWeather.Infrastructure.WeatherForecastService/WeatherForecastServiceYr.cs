using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NeverBadWeather.DomainModel;
using NeverBadWeather.DomainModel.Exception;
using NeverBadWeather.DomainServices;
using NeverBadWeather.Infrastructure.WeatherForecastService.Model;
using NeverBadWeather.Infrastructure.WeatherForecastService.Properties;

namespace NeverBadWeather.Infrastructure.WeatherForecastService
{
    public class WeatherForecastServiceYr : IWeatherForecastService
    {

        public async Task<WeatherForecast> GetWeatherForecast(Place place)
        {
            var url = $"http://www.yr.no/sted/"
                      + place.Country + "/"
                      + place.Region + "/"
                      + place.City + "/"
                      + place.Name + "/varsel.xml";
            using var wc = new WebClient();
            var xml = await wc.DownloadStringTaskAsync(new Uri(url));
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(weatherdata));
                using var reader = new StringReader(xml);
                var weatherData = (weatherdata) xmlSerializer.Deserialize(reader);

                var forecasts = weatherData.forecast.tabular.Select(TemperatureForecastFromXml);
                return new WeatherForecast(forecasts);
            }
            catch (Exception e)
            {
                throw new CouldNotFetchWeatherForecastException(e);
            }
        }

        private static TemperatureForecast TemperatureForecastFromXml(weatherdataForecastTime data)
        {
            var temperature = data.temperature.value;
            return new TemperatureForecast(temperature, data.from, data.to);
        }

        public IEnumerable<Place> GetAllPlaces()
        {
            var lines = Resources.noreg.Split(Environment.NewLine).Skip(1);
            return lines.Select(PlaceFromCsvLine).ToList();
        }

        private static Place PlaceFromCsvLine(string line)
        {
            var fields = line.Split('\t');
            var location = new Location(fields[8], fields[9]);
            return new Place("Norge", fields[7], fields[6], fields[1], location);
        }
    }
}
