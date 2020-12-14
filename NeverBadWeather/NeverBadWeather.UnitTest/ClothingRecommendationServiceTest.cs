using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NeverBadWeather.ApplicationServices;
using NeverBadWeather.DomainModel;
using NeverBadWeather.DomainServices;
using NUnit.Framework;

namespace NeverBadWeather.UnitTest
{
    public class ClothingRecommendationServiceTest
    {

        [Test]
        public async Task TestHappyCase()
        {
            // arrange
            var testDate = new DateTime(2000, 1, 1, 10, 0, 0);
            var testPeriod = new TimePeriod(testDate, testDate.AddHours(10));
            var testLocation = new Location(59, 10);

            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            var mockClothingRuleRepository = new Mock<IClothingRuleRepository>();

            mockClothingRuleRepository
                .Setup(crr => crr.GetRulesByUser(It.IsAny<Guid?>()))
                .ReturnsAsync(new[]
                {
                    new ClothingRule(-20, 10, null, "Bobledress"),
                    new ClothingRule(10, 20, null, "Bukse og genser"),
                    new ClothingRule(20, 40, null, "T-skjore og shorts"),
                });


            mockWeatherForecastService
                .Setup(fs => fs.GetAllPlaces())
                .Returns(new[] { new Place("", "", "", "Andeby", new Location(59.1f, 10.1f)), });

            mockWeatherForecastService
                .Setup(fs => fs.GetWeatherForecast(It.IsAny<Place>()))
                .ReturnsAsync(new WeatherForecast(new[] {
                    new TemperatureForecast(25,testDate.AddHours(2), testDate.AddHours(4)),
                }));

            // act
            var request = new ClothingRecommendationRequest(testPeriod, testLocation);
            var service = new ClothingRecommendationService(
                mockWeatherForecastService.Object,
                mockClothingRuleRepository.Object);
            var recommendation = await service.GetClothingRecommendation(request);

            // assert
            Assert.AreEqual("Andeby", recommendation.Place.Name);
            Assert.That(recommendation.Rules, Has.Exactly(1).Items);
            var rule = recommendation.Rules.First();
            Assert.AreEqual("T-skjore og shorts", rule.Clothes);
        }

        [Test]
        public async Task TestCreateRule()
        {
            var mockRuleRepository = new Mock<IClothingRuleRepository>();
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
           
        
            mockRuleRepository.Setup(rr => rr.Create(It.IsAny<ClothingRule>())).ReturnsAsync(1);
            var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockRuleRepository.Object);
            var newRule = new ClothingRule(100,200,false,"bærepose");
           
            var ruleCreated = await service.CreateRule(newRule);
            Assert.IsTrue(ruleCreated);
            mockRuleRepository.Verify(rr => rr.Create(It.Is<ClothingRule>(c=>c.Clothes =="bærepose" && c.IsRaining == false)));
        
            

        }

        [Test]
        public async Task TestCreateAndUpdateRules()
        {
            var mockRuleRepository = new Mock<IClothingRuleRepository>();
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();

            mockRuleRepository.Setup(rr => rr.Create(It.IsAny<ClothingRule>())).ReturnsAsync(1);
            var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockRuleRepository.Object);
            var id = "b5b6c634-bc43-4c41-8d90-74c05c5567df";
            var newrule = new ClothingRule(150,350, true, "BurnFeelsSoGood", new Guid(id));
            var ruleCreated = await service.CreateRule(newrule);
            Assert.IsTrue(ruleCreated);
            mockRuleRepository.Setup(rr => rr.Update(It.IsAny<ClothingRule>())).ReturnsAsync(1);
            mockRuleRepository.Verify(rr=>rr.Update(It.Is<ClothingRule>(c =>c.Clothes == "BurnFeelsSoGood")));


            var ruleUpdate = new ClothingRule(160,370,true, "AllTheBurns", new Guid(id));
            var ruleUpdated = await service.UpdateRule(ruleUpdate);
            Assert.IsTrue(ruleUpdated);
            var wantedtemp = 160;
            mockRuleRepository.Verify(rr=>rr.Update(It.Is<ClothingRule>(c =>c.Clothes =="AllTheBurns" && c.Id == new Guid(id))));
            mockRuleRepository.Verify(rr =>rr.Update(It.Is<ClothingRule>(c=>c.FromTemperature == wantedtemp)));
            
        }

        [Test]

        public async Task TestInvalid()
        {
            var mockRuleRepository = new Mock<IClothingRuleRepository>();
            var mockWeatherServce = new Mock<IWeatherForecastService>();

            mockRuleRepository.Setup(rr => rr.Create(It.IsAny<ClothingRule>())).ReturnsAsync(1);
            var  invalidrule = new ClothingRule(-100000000, -0, null, null, null);
            var service = new ClothingRecommendationService(mockWeatherServce.Object, mockRuleRepository.Object);
            var isCreated = await service.CreateRule(invalidrule);
            Assert.IsFalse(isCreated);
        }

 
        
      

    }
}