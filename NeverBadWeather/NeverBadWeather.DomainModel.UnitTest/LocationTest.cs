using NUnit.Framework;

namespace NeverBadWeather.DomainModel.UnitTest
{
    public class LocationTest
    {
        [Test]
        public void TestIsWithinInside()
        {
            // arrange
            var corner1 = new Location(0, 0);
            var corner2 = new Location(1, 1);
            var location = new Location(0.5f, 0.5f);

            // act
            var isWithin = location.IsWithin(corner1, corner2);

            // assert
            Assert.IsTrue(isWithin);
        }

        [Test]
        public void TestIsInvalidWithinInside()
        {
            var corner1  = new Location(1500000000000000, 3512131321321321);
            var corner2 =  new Location(-158484684684684684, 13213213213213213131);
            var location = new Location(0.5f, 0.5f);

            var isWithin = location.IsWithin(corner1, corner2);
            Assert.IsFalse(isWithin);
        }

        [Test]
        public void TestGetDistance()
        {
            var location1 = new Location(59.13118f, 10.21665f);
            var location2 = new Location(59.05328f, 10.03517f);
            var LtoS = location2.GetDistanceFrom(location1);
            var StoL = location1.GetDistanceFrom(location2);

            Assert.AreEqual(LtoS, StoL);
        }
    }
}