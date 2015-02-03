using System;

namespace AutoSky.CoOrdinate_Systems
{
    /// <summary>
    /// Summary description for Equatorial_CS
    /// </summary>
    public class EquatorialCs
    {
        protected bool Equals(EquatorialCs other)
        {
            return RightAscension.Equals(other.RightAscension) && Declination.Equals(other.Declination) && HourAngle.Equals(other.HourAngle) && Equals(RaArgs, other.RaArgs) && Equals(DecArgs, other.DecArgs);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = RightAscension.GetHashCode();
                hashCode = (hashCode*397) ^ Declination.GetHashCode();
                hashCode = (hashCode*397) ^ HourAngle.GetHashCode();
                hashCode = (hashCode*397) ^ (RaArgs != null ? RaArgs.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (DecArgs != null ? DecArgs.GetHashCode() : 0);
                return hashCode;
            }
        }

        public readonly double RightAscension;
        public readonly double Declination;
        public readonly double HourAngle;
        public readonly double[] RaArgs;
        public readonly double[] DecArgs;

        public EquatorialCs(double rightAscension, double declination, double hourAngle)
        {
            RightAscension = rightAscension;
            Declination = declination;
            HourAngle = hourAngle;
        }
        public EquatorialCs(double rightAscension, double declination)
        {
            RightAscension = rightAscension;
            Declination = declination;
        }
        public EquatorialCs(double raH, double raM, double raS, double decDegree, double decMinute, double decSecond)
        {
            RaArgs = new[] { raH, raM, raS };
            DecArgs = new[] { decDegree, decMinute, decSecond };
        }
        public EquatorialCs(double[] ra, double[] dec)
        {
            RaArgs = ra;
            RightAscension = ra[0];
            DecArgs = dec;
            Declination = dec[0];
        }
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EquatorialCs) obj);
        }
    }
}