namespace AutoSky
{
    /// <summary>
    /// Summary description for CommandStaticStrings
    /// </summary>
    internal static class CommandStaticStrings
    {
        public static string GetGpsCommand = "GPS_GET;";
        public static string GetOrnCommand = "ORN_GET;";
        public static string SetOrnCommand = "ORN_SET:{0:0.00},{1:0.00};";
        public static string StartMoveCommand = "DIR_SET:{0};";
        public static string StopMoveCommand = "DIR_STOP;";
    }
}