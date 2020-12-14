using System;
using System.Collections.Generic;
using System.Text;
using NeverBadWeather.DomainModel.Exception;
using NUnit.Framework;

namespace NeverBadWeather.DomainModel.UnitTest
{
    class TempratureStatisticsTest
    {
        [Test]
        public void TestAddTemprature()
        {
           byte ftemp1 = 1;
           byte ftemp2 = 2;
           var TempratureStatistics = new TemperatureStatistics();
           TempratureStatistics.AddTemperature(ftemp1);
           TempratureStatistics.AddTemperature(ftemp2);
           Assert.AreEqual(1, TempratureStatistics.Min);
           Assert.AreEqual(2, TempratureStatistics.Max);
        }
        [Test]
        public void TEstTempratureWithNoNumbersException()
        {
            Assert.Throws<CannotGiveMinOrMaxWithNoNumbersException>(() => TestNoNumbersException());

        }

        private void TestNoNumbersException()
        {
            var tempstats = new TemperatureStatistics();
            Assert.AreEqual(1, tempstats.Min);
            Assert.AreEqual(1, tempstats.Max);
        }
        
    
    }
}
