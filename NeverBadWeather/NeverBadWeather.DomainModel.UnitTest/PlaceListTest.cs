using System;
using System.Collections.Generic;
using System.Text;
using NeverBadWeather.DomainModel.Exception;
using NUnit.Framework;

namespace NeverBadWeather.DomainModel.UnitTest
{
    class PlaceListTest
    {
        [Test]
        public void TestLoadIsTrue()
        {
            var location = new Location(5913118, 1021665);
            var place = new Place("norway", "vestfold", "sandefjord", "Sandefjord", location);
           
            
            var placelist = new PlaceList();
            placelist.Load(new[] {place});
            Assert.IsTrue(placelist.IsLoaded);
        }

        [Test]
        public void TestLoadISFalse()
        {
            var placelist = new PlaceList();
             Assert.IsFalse(placelist.IsLoaded);

        }

        [Test]
        public void TestPlaceIsNotLoadedException()
        {       //tests that method TestGetClosestPlaceException() Trows an PlaceListNotLoadedException
            Assert.Throws<PlaceListNotLoadedException>(() => TestGetClosestPlaceException());
        }

        private void TestGetClosestPlaceException()
        {
            var PlaceList = new PlaceList();
            var location = new Location(0, 0);
            PlaceList.GetClosestPlace(location);
        }

        [Test]
        public void TestGetClosestPlaceOk()
        {
            var location = new Location(5913118, 1021665);
            var place = new Place("Norway", "Vestfold", "Sandefjord", "Sandefjord", location);
            var placeList = new PlaceList();
            placeList.Load(new[] { place });
            var closestPlace = placeList.GetClosestPlace(location);
            Assert.AreEqual(place, closestPlace);
        }

     
    }
}
