using System;

namespace AutoSky.CoOrdinate_Systems
{
    /// <summary>
    /// Summary description for GoogleSky_hcs
    /// </summary>
    public class GoogleSkyCs
    {
        protected bool Equals(GoogleSkyCs other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }

        public double Latitude;
        public double Longitude;
        public double Range;

        public GoogleSkyCs(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

        }
        public GoogleSkyCs(double latitude, double longitude, double range)
        {
            Latitude = latitude;
            Longitude = longitude;
            Range = range;
        }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GoogleSkyCs)obj);
        }

    }
}