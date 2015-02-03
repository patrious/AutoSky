using System;
using AutoSky.CoOrdinate_Systems;

namespace AutoSky
{
    /// <summary>
    /// Summary description for CoordinateConverter
    /// </summary>
    public static class CoordinateConverter
    {

        private static double latitude = double.MaxValue;
        private static double longitude = double.MaxValue;

        public static double Latitude
        {
            get
            {
                if (latitude == double.MaxValue)
                    latitude = -80.548390;
                //throw new Exception("gps latitude was never set properly, and is being used");

                return latitude;
            }
            set { latitude = value; }
        }
        public static double Longitude
        {
            get
            {
                if (longitude == double.MaxValue)
                    longitude = 43.454788;
                //throw new Exception("gps longitude was never set properly, and is being used");

                return longitude;
            }
            set { longitude = value; }
        }

        public static double LocalSiderealTime
        {
            get
            {
                var thisyear = new DateTime(DateTime.Now.Year, 1, 1);
                const double A = 0.0657098;
                const double B = 19.41409;
                const double C = 1.002737909;
                const double meanLong = 82.5;
                var currentTime = DateTime.Now.Hour + DateTime.Now.Minute / 60.0;
                var t = currentTime - (4 * (meanLong - Longitude)) / 60;
                var d = (DateTime.Now - thisyear).TotalDays;
                var st = (d * A) - B + (t * C);
                if (st < 0) st += 24;
                if (st > 24) st -= 24;
                return st;
            }
        }


        private static double RadiansToDegree(double n)
        {
            return (n * 180) / Math.PI;
        }
        private static double DegreeToRadians(double n)
        {
            return (n * Math.PI) / 180;
        }

        public static HorizontalCs EquatorialToHorizontal(EquatorialCs ecs)
        {
            var tHourAngle = DegreeToRadians(ecs.HourAngle * 15);
            var tDeclination = DegreeToRadians(ecs.Declination);
            var tempLatitude = DegreeToRadians(Latitude);
            var fSinAlt = (Math.Sin(tDeclination) * Math.Sin(tempLatitude)) + (Math.Cos(tDeclination) * Math.Cos(tempLatitude) * Math.Cos(tHourAngle));
            var altitude = Math.Asin(fSinAlt);
            var fCosAzim = ((Math.Sin(tDeclination) - (Math.Sin(tempLatitude) * Math.Sin(altitude))) / (Math.Cos(tempLatitude) * Math.Cos(altitude)));
            var azimuth = RadiansToDegree(Math.Acos(fCosAzim));
            //if (Math.Sin(tHourAngle) > 0)
            //{
            //    azimuth = 360 - azimuth;
            //}
            altitude = RadiansToDegree(altitude);

            return new HorizontalCs(azimuth, altitude);

        }
        public static EquatorialCs HorizontalToEquatorial(HorizontalCs hcs)
        {
            var tAltitude = DegreeToRadians(hcs.Altitude);
            var tAzimuth = DegreeToRadians(hcs.Azimuth);
            var tempLatitude = DegreeToRadians(Latitude);
            var fSinDecl = (Math.Sin(tAltitude) * Math.Sin(tempLatitude)) + (Math.Cos(tAltitude) * Math.Cos(tempLatitude) * Math.Cos(tAzimuth));
            var declination = Math.Asin(fSinDecl);
            var fCosH = ((Math.Sin(tAltitude) - (Math.Sin(tempLatitude) * Math.Sin(declination))) / (Math.Cos(tempLatitude) * Math.Cos(declination)));
            var hourAngle = RadiansToDegree(Math.Acos(fCosH));
            //if (Math.Sin(tAzimuth) > 0)
            //{
            //    hourAngle = 360 - hourAngle;
            //}

            declination = RadiansToDegree(declination);
            hourAngle = hourAngle / 15.0;
            var rightAscension = LocalSiderealTime - hourAngle;
            return new EquatorialCs(rightAscension, declination, hourAngle);
        }
        public static EquatorialCs GoogleSkyToEquatorial(GoogleSkyCs gcs)
        {
            var declination = gcs.Latitude;
            var rightAscension = (gcs.Longitude + 180) / 15;
            var hourAngle = LocalSiderealTime - rightAscension;
            return new EquatorialCs(rightAscension, declination, hourAngle);
        }
        public static GoogleSkyCs EquatorialToGoogleSky(EquatorialCs ecs)
        {
            var localLatitude = ecs.Declination;
            var localLongitude = ecs.RightAscension * 15 - 180;
            return new GoogleSkyCs(localLatitude, localLongitude);
        }
        public static HorizontalCs GoogleSkyToHorizonatal(GoogleSkyCs gcs)
        {
            var ecs = GoogleSkyToEquatorial(gcs);
            return EquatorialToHorizontal(ecs);
        }
        public static GoogleSkyCs HorizontalToGoogleSky(HorizontalCs hcs)
        {
            var ecs = HorizontalToEquatorial(hcs);
            var gcs = EquatorialToGoogleSky(ecs);
            return gcs;
        }


        public static EquatorialCs CreateEquatorialInstance(double declination, double hourangle)
        {
            return new EquatorialCs(LocalSiderealTime - hourangle, declination, hourangle);
        }
        public static EquatorialCs CreateEquatorialInstance2(double rightAcension, double declination)
        {
            return new EquatorialCs(rightAcension, declination, LocalSiderealTime - rightAcension);
        }
        public static EquatorialCs CreateEquatorialFromMinuteofArc(double[] raArgs, double[] decArgs)
        {

            var ra = (raArgs[0] + raArgs[1] / (60) + raArgs[2] / 3600);
            var dec = (decArgs[0] + decArgs[1] / (60) + decArgs[2] / 3600);
            return new EquatorialCs(ra, dec);

        }

        public static double CalculateRange(double[] MOA_ArcDegree_args)
        {
            var decimalArcDegree = (MOA_ArcDegree_args[0] + MOA_ArcDegree_args[1] / (60) + MOA_ArcDegree_args[2] / 3600);
            var range = 6.378 * Math.Pow(10, 6) * (1.1917536 * Math.Sin(decimalArcDegree / 2) - Math.Cos(decimalArcDegree / 2) + 1);
            return range;
        }
    }
}