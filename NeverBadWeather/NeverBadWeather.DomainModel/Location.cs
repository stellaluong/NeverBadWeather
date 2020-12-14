using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NeverBadWeather.DomainModel
{
    public class Location
    {
        public float Latitude { get; }
        public float Longitude { get; }

        public Location(string latitude, string longitude)
        : this(ToFloat(latitude), ToFloat(longitude))
        {
        }

        public Location(float latitude, float longitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        private static float ToFloat(string longitude)
        {
            return Convert.ToSingle(longitude, CultureInfo.InvariantCulture);
        }


        public double GetDistanceFrom(Location location)
        {
            var deltaLat = location.Latitude - Latitude;
            var deltaLon = location.Longitude - Longitude;
            return Math.Sqrt(deltaLon * deltaLon + deltaLat * deltaLat);
        }

        public Location CreateWithDelta(float deltaLat, float deltaLon)
        {
            return new Location(Latitude + deltaLat, Longitude + deltaLon);
        }

        public bool IsWithin(Location min, Location max)
        {
            return Latitude >= min.Latitude && Latitude <= max.Latitude 
                && Longitude >= min.Longitude && Longitude <= max.Longitude;
        }
    }
}
