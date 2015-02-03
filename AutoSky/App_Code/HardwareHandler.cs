using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading;
using AutoSky.CoOrdinate_Systems;

namespace AutoSky
{
    /// <summary>
    ///     Summary description for HardwareHandler
    /// </summary>
    public class HardwareHandler
    {
        public delegate void MessageHandler(CustomEventArgs a);

        public Boolean ConnectedToSky = false;
        private readonly HardwareCommunicator comm;

        public event MessageHandler ArduinoMessageEvent;

        public HardwareHandler()
        {
            comm = new HardwareCommunicator(this);
        }
        /// <summary>
        ///     Establish a connection from this application to our telescope.
        /// </summary>
        public void ConnectToArduino()
        {
            comm.ConnectToArduino();
        }

        public void FireEvent(CustomEventArgs args)
        {
            var connectionEvent = args as ConnectionEvent;
            if (connectionEvent != null)
            {
                var obj = connectionEvent;
                ConnectedToSky = obj.Connected;
            }
            ArduinoMessageEvent(args);
        }

        /// <summary>
        ///     Get location of telescope.
        /// </summary>
        public void GetGpsCoOrdinates()
        {
            if (!CheckConnection()) return;
            comm.AddToQueue(CommandStaticStrings.GetGpsCommand);
        }

        /// <summary>
        ///     Request Orientation of the Telescope.
        /// </summary>
        public void GetOrientation()
        {
            if (!CheckConnection()) return;
            comm.AddToQueue(CommandStaticStrings.GetOrnCommand);
        }

        private OrientationEvent or;
        Semaphore semi = new Semaphore(0, 1);
        public bool stop = false;
        /// <summary>
        ///     Request the AutoSky Telescope to point to a new direction.
        /// </summary>
        /// <param name="coordinate"> the GoogleSky coordinate you wish to use</param>
        /// <param name="telescopeUp"></param>
        /// <param name="telescopeDown"></param>
        public void RequestNewPosition(GoogleSkyCs coordinate)
        {
            //ArduinoMessageEvent += RetrieveOrientationEvent();
            stop = false;
            if (!CheckConnection()) return;
            var destinationHcs = CoordinateConverter.GoogleSkyToHorizonatal(coordinate);
            var altitude = destinationHcs.Altitude;
            var azimuth = destinationHcs.Azimuth;
            //First, set the altitude using the 3axis data
            //Y arrow points towards front of telescope. Z arrow points straight up. (X axis not used.)
            const int maxIterations = 500;
            for (var i = 0; i < maxIterations; i++)
            {
                if (stop) break;
                ArduinoMessageEvent += OnArduinoMessageEvent;
                GetOrientation();
                semi.WaitOne(800);
                ArduinoMessageEvent -= OnArduinoMessageEvent;
                if (or == null)
                {
                    ArduinoMessageEvent(new ErrorEvent("Didn't get orientation Data!"));
                    StopMotors();
                    continue;
                }
                var gcs = new GoogleSkyCs(or.Orientation.Latitude, or.Orientation.Longitude);

                or = null;
                var currentHcs = CoordinateConverter.GoogleSkyToHorizonatal(gcs);
                var currentAlt = currentHcs.Altitude;
                var currentHeading = currentHcs.Azimuth;
                ArduinoMessageEvent(new ErrorEvent(String.Format("Alt: {0},Azi: {1}", currentAlt, currentHeading)));
                //Too close to adjust    
                var altComplete = false;
                if (Math.Abs(altitude - currentAlt) < 5)
                {
                    if (Math.Abs(altitude - currentAlt) < 3)
                    {
                        UpDownStop();
                        altComplete = true;
                    }
                    else
                    {
                        altComplete = false;
                        if (altitude > currentAlt)
                        {
                            telescopeUp();
                        }
                        else
                        {
                            telescopeDown();
                        }
                    }
                }
                else
                {
                    altComplete = false;
                    if (altitude > currentAlt)
                    {
                        telescopeUp();
                    }
                    else
                    {
                        telescopeDown();
                    }
                }
                //Too close to adjust
                var aziComplete = false;
                if (Math.Abs(azimuth - currentHeading) < 40)
                {
                    if (Math.Abs(azimuth - currentHeading) < 10)
                    {
                        LeftRightStop();
                        aziComplete = true;
                    }
                    else
                    {
                        aziComplete = false;
                        var a = azimuth - currentHeading;
                        var b = currentHeading - azimuth;
                        if (a < 0)
                        {
                            a = a + 360;
                        }
                        if (b < 0)
                        {
                            b = b + 360;
                        }
                        if (a < b)
                        {
                            telescopeRightSlow();
                        }
                        else
                        {
                            telescopeLeftSlow();
                        }
                    }

                }
                else
                {
                    aziComplete = false;
                    var a = azimuth - currentHeading;
                    var b = currentHeading - azimuth;

                    if (a < b)
                    {
                        telescopeRight();
                    }
                    else
                    {
                        telescopeLeft();
                    }
                }
                if (!aziComplete || !altComplete) continue;
                ArduinoMessageEvent(new ErrorEvent("Success! Pointing where we want to!"));
                StopMotors();
                break;
            }
            StopMotors();
        }

        private void OnArduinoMessageEvent(CustomEventArgs customEventArgs)
        {
            var args = customEventArgs as OrientationEvent;
            if (args == null) return;
            or = args;
            semi.Release();
        }

        private void LeftRightStop()
        {
            StartMovement(AutoSky.UDLR.StopLr);
        }
        private void UpDownStop()
        {
            StartMovement(AutoSky.UDLR.StopUd);
        }
        private void StopMotors()
        {
            StartMovement(AutoSky.UDLR.Stop);
        }

        private void telescopeRightSlow()
        {
            StartMovement(AutoSky.UDLR.RightSlow);
        }
        private void telescopeLeftSlow()
        {
            StartMovement(AutoSky.UDLR.LeftSlow);
        }
        private void telescopeRight()
        {
            StartMovement(AutoSky.UDLR.Right);
        }
        private void telescopeLeft()
        {
            StartMovement(AutoSky.UDLR.Left);
        }
        private void telescopeUp()
        {
            StartMovement(AutoSky.UDLR.Up);
        }
        private void telescopeDown()
        {
            StartMovement(AutoSky.UDLR.Down);
        }

        public void StartMovement(AutoSky.UDLR direction)
        {
            if (!CheckConnection()) return;

            var command = string.Format(CommandStaticStrings.StartMoveCommand, (int)direction);
            comm.AddToQueue(command);
        }

        public void StopMovement()
        {
            if (!CheckConnection()) return;
            comm.AddToQueue(CommandStaticStrings.StopMoveCommand);
        }

        public void RecieveData(string returnData)
        {
            var data = returnData.Split(new[] { ':' });
            switch (data[0])
            {
                //AutoSky GPS Co-Ordinates
                case "GPS":
                    HandleGps(data);
                    break;
                //AutoSky Current Orientation
                case "ORN":
                    HandleOrn(data);
                    break;
                //AutoSky Completed Move move
                case "MOV":
                    HandleMov(data);
                    break;
                case "CON":
                    HandleCon();
                    break;
            }
        }

        private void HandleCon()
        {
            //Notify UI that connection has been established.
            ConnectedToSky = true;
            ArduinoMessageEvent(new ConnectionEvent(true));
        }

        public void CloseConnection()
        {
            comm.Dispose();
        }

        /// <summary>
        ///     GPS Data as sent by arduino. Parse it correctly and return it to the UI.
        /// </summary>
        private void HandleGps(string[] inData)
        {
            if (inData == null) throw new ArgumentNullException("inData");
            //data[0] = "GPS"
            //data[1] = 40|23.9087,80|41.1543   <- conver this into 40 degrees, 23.9087 minutes

            //Send Up
            var data = inData[1].Split(new[] { ',' });
            var latDegree = int.Parse(data[0].Substring(0, 2));
            if (latDegree == -1)
            {
                ArduinoMessageEvent(new ErrorEvent("Telescope could not pinpoint GPS coOrdinates"));
                return;
            }
            var latMinutes = float.Parse(data[0].Substring(2, data[1].Length - 2));

            var longDegree = int.Parse(data[1].Substring(0, 2));
            //float long_minutes = float.Parse(data[1].Substring(2, data[1].Length - 2));

            double localLatitude = latDegree + latMinutes / 60;
            double localLongitude = longDegree + latMinutes / 60;
            CoordinateConverter.Latitude = localLatitude;
            CoordinateConverter.Longitude = localLongitude;

            ArduinoMessageEvent(new GpsEvent(localLongitude, localLatitude));
        }
        /// <summary>
        ///     Handle the Orientation Data
        /// </summary>
        /// <param name="data"></param>
        private void HandleOrn(string[] data)
        {
            var aAndM = data[1].Split(new[] { ';' });
            var aData = aAndM[0].Split(new[] { ',' });
            var mData = aAndM[1].Split(new[] { ',' });

            var altitudeprep = new Point3D(aData[0], aData[1], aData[2]);
            var azimuthPrep = new Point3D(mData[0], mData[1], mData[2]);


            var altitude = CalcuclateAltitude(altitudeprep);
            var azimuth = CalculateAzimuth(azimuthPrep);

            //Convert this into the Orientation neeed for google sky. 
            var hcs = new HorizontalCs(azimuth, altitude);
            try
            {
                ArduinoMessageEvent(new ErrorEvent(string.Format("Alt {0} Azi {1}", altitude, azimuth)));
                var gcs = CoordinateConverter.HorizontalToGoogleSky(hcs);

                //Pass Data up to UI via event or something.
                ArduinoMessageEvent(new OrientationEvent(gcs));
            }
            catch (Exception ex)
            {
                ArduinoMessageEvent(new ErrorEvent(string.Format("Error Occurred: {0}", ex.Message)));
                if (ex.Message.Contains("gps"))
                {
                    GetGpsCoOrdinates();
                }
            }
        }

        /// <summary>
        ///     Function to handle the Completed Move Data
        /// </summary>
        /// <param name="data"></param>
        private void HandleMov(string[] data)
        {
            //Data[0] = "MOV"
            //Data[1] = 0/1
            //0 -> Telescope failed to reach orientation
            //1 -> Telescope success to reach orientation

            bool result;
            if (Boolean.TryParse(data[1], out result))
            {
                //Wasn't able to determine result
                //arduinoMessageEvent(null, new MovementEvent(false));
                throw new Exception("Unable to determine if telescope orienteded properly. Issues with message.");
            }

            //tell UI result
            ArduinoMessageEvent(new MovementEvent(result));
        }

        #region HelperFunctions

        /// <summary>
        ///     Convert the accelerometer x,y,z data into an altitude value
        /// </summary>
        /// <param name="a">a 3D point containing accelerometer data</param>
        /// <returns></returns>
        public double CalcuclateAltitude(Point3D a)
        {
            return -1 * (Math.Atan2(a.Y, a.Z) * 180.0) / Math.PI;
        }

        /// <summary>
        ///     Convert the magnetometer x,y,z data into an azimuth value
        /// </summary>
        /// <param name="m">a 3D point containing magnetometer data</param>
        /// <returns></returns>
        public double CalculateAzimuth(Point3D m)
        {
            var heading = (Math.Atan2(m.X, m.Y) * 180.0) / Math.PI;
            if (heading < 0)
                heading += 360;
            return heading;
        }

        private bool CheckConnection()
        {
            if (ConnectedToSky) return true;
            ArduinoMessageEvent(new ConnectionEvent(false));
            return false;
        }

        #endregion
    }
}