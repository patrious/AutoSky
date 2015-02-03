namespace AutoSky
{
    public class GpsEvent : CustomEventArgs
    {
        public double Latitude;
        public double Longitude;
        public GpsEvent(double longitude, double latitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

    }
}