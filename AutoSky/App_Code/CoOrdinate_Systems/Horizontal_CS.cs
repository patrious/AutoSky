using System;

namespace AutoSky.CoOrdinate_Systems
{
    /// <summary>
    /// Summary description for Horizontal_CS
    /// </summary>
    public class HorizontalCs
    {
        protected bool Equals(HorizontalCs other)
        {
            return Azimuth.Equals(other.Azimuth) && Altitude.Equals(other.Altitude);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Azimuth.GetHashCode()*397) ^ Altitude.GetHashCode();
            }
        }

        public readonly double Azimuth;
        public readonly double Altitude;

        public HorizontalCs(double azimuth, double altitude)
        {
            Azimuth = azimuth;
            Altitude = altitude;
        }



        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((HorizontalCs) obj);
        }
    
    }
}