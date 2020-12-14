using System;
using System.Collections.Generic;
using System.Text;

namespace NeverBadWeather.DomainModel
{
    public class ClothingRecommendation
    {
        public IEnumerable<ClothingRule> Rules {get;}
        public WeatherForecast WeatherForecast { get;}
        public Place Place { get; }

        public ClothingRecommendation(IEnumerable<ClothingRule> rules, WeatherForecast weatherForecast, Place place)
        {
            Rules = rules;
            WeatherForecast = weatherForecast;
            Place = place;
        }
    }
}
