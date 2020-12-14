using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NeverBadWeather.DomainModel.UnitTest
{
    class ClothingRuleTest
    {
        [Test]
        public void TestClothingRuleMatchTrue()
        {
            var clothingRule = new ClothingRule(100,200,false,"Bærepose",null);
            var temperatureStatistics = new TemperatureStatistics();
            temperatureStatistics.AddTemperature(110);
            temperatureStatistics.AddTemperature(190);
            
            var match = clothingRule.Match(temperatureStatistics);
            Assert.IsTrue(match);
        }
        [Test]
        public void TestClothingRuleMatchFalse()
        {
            var clothingRule = new ClothingRule(100, 200, false, "Bærepose", null);
            var temperatureStatistics = new TemperatureStatistics();
            temperatureStatistics.AddTemperature(90);
            temperatureStatistics.AddTemperature(210);
            var match = clothingRule.Match(temperatureStatistics);
            Assert.IsFalse(match);
        }
        //[Test]
        //public void TestClothingRuleMatchWithNegativeNumbersTrue()
        //{
        //    var clothingRule = new ClothingRule(-140 ,-160, false, "Bærepose", null);
        //    var temperatureStatistics = new TemperatureStatistics();
        //    temperatureStatistics.AddTemperature(-130);
        //    temperatureStatistics.AddTemperature(-170);

        //    var match = clothingRule.Match(temperatureStatistics);
        //    Assert.IsTrue(match);
        //}


    }
}
