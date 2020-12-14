using System;
using System.Collections.Generic;
using System.Text;
using NeverBadWeather.DomainModel.Exception;

namespace NeverBadWeather.DomainModel
{
    public class TemperatureStatistics
    {
        private int _min;
        private int _max;

        public int Min
        {
            get
            {
                if (_hasNoInput)throw new CannotGiveMinOrMaxWithNoNumbersException();
                return _min;
            }
        }

        public int Max
        {
            get
            {
                if (_hasNoInput) throw new CannotGiveMinOrMaxWithNoNumbersException();
                return _max;
            }
        }

        private bool _hasNoInput = true;

        public void AddTemperature(int temperature)
        {
            if (_hasNoInput)
            {
                _max = _min = temperature;
                _hasNoInput = false;
                return;
            }
            _max = Math.Max(_max, temperature);
            _min = Math.Min(_min, temperature);
        }
    }
}
