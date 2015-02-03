using AutoSky.CoOrdinate_Systems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoSky.TestFolder
{
    [TestClass]
    public class CoordinateConverterTests
    {
// ReSharper disable once InconsistentNaming
        private const double DELTA = 0.00000001;

        public CoordinateConverterTests()
        {
            CoordinateConverter.Latitude = 43.56447;
            CoordinateConverter.Longitude = 50;
        }

        [TestMethod]
        public void GoogleSkyToHorizontalAndBack()
        {
            var start = new GoogleSkyCs(5, 6);
            var middle = CoordinateConverter.GoogleSkyToHorizonatal(start);
            var end = CoordinateConverter.HorizontalToGoogleSky(middle);
            Assert.AreEqual(start.Latitude, end.Latitude, DELTA);
            Assert.AreEqual(start.Longitude, end.Longitude, DELTA);
        
        }

        [TestMethod]
        public void GoogleSkyToGodKnowsAndBack()
        {
            var start = new GoogleSkyCs(180, -180);
            var a = CoordinateConverter.GoogleSkyToHorizonatal(start);
            var b = CoordinateConverter.HorizontalToGoogleSky(a);
            var c = CoordinateConverter.GoogleSkyToHorizonatal(b);
            var d = CoordinateConverter.HorizontalToGoogleSky(c);
            var e = CoordinateConverter.GoogleSkyToHorizonatal(d);
            var f = CoordinateConverter.HorizontalToGoogleSky(e);
            var g = CoordinateConverter.GoogleSkyToHorizonatal(f);              
            var end = CoordinateConverter.HorizontalToGoogleSky(g);
            Assert.AreEqual(start.Latitude, end.Latitude, DELTA);
            Assert.AreEqual(start.Longitude, end.Longitude, DELTA);

        }
        [TestMethod]
        public void GoogleSkyToEquatorialAndBack()
        {
            var start = new GoogleSkyCs(5, 5);
            var middle = CoordinateConverter.GoogleSkyToEquatorial(start);
            var end = CoordinateConverter.EquatorialToGoogleSky(middle);

            Assert.AreEqual(start.Latitude, end.Latitude, DELTA);
            Assert.AreEqual(start.Longitude, start.Longitude, DELTA);
        }

        [TestMethod]
        public void HotizontalToEquatorialAndBack()
        {
            var start = new HorizontalCs(0, 0);
            var middle = CoordinateConverter.HorizontalToEquatorial(start);
            var end = CoordinateConverter.EquatorialToHorizontal(middle);

            Assert.AreEqual(0, end.Azimuth, DELTA);
            Assert.AreEqual(start.Altitude, start.Altitude, DELTA);
        }

        [TestMethod]
        public void HorizontalToGoogleAndBackFull()
        {
            var s1 = new HorizontalCs(360, 90);
            var start = CoordinateConverter.HorizontalToEquatorial(s1);
            var middle = CoordinateConverter.EquatorialToGoogleSky(start);
            var end = CoordinateConverter.GoogleSkyToHorizonatal(middle);

            Assert.AreEqual(s1.Altitude, end.Altitude, DELTA);
            Assert.AreEqual(s1.Azimuth, end.Azimuth, DELTA);
        }

        [TestMethod]
        public void HorizontalToGoogleAndBackMinimum()
        {
            var s1 = new HorizontalCs(0, 0);
            var start = CoordinateConverter.HorizontalToEquatorial(s1);
            var middle = CoordinateConverter.EquatorialToGoogleSky(start);
            var end = CoordinateConverter.GoogleSkyToHorizonatal(middle);

            Assert.AreEqual(s1.Altitude, end.Altitude, DELTA);
            Assert.AreEqual(0, end.Azimuth, DELTA);
        }

        [TestMethod]
        public void EquatorialToHorizontalAndBack()
        {
            var s1 = new HorizontalCs(360, 90);
            var start = CoordinateConverter.HorizontalToEquatorial(s1);
            var middle = CoordinateConverter.EquatorialToHorizontal(start);
            var end = CoordinateConverter.HorizontalToEquatorial(middle);

            Assert.AreEqual(start.RightAscension, end.RightAscension, DELTA);
            Assert.AreEqual(start.HourAngle, start.HourAngle, DELTA);
            Assert.AreEqual(start.Declination, start.Declination, DELTA);
        }


    }
}
