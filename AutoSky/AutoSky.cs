using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoSky.CoOrdinate_Systems;
using AutoSky.App_Code;
using System.Text.RegularExpressions;

namespace AutoSky
{
    public partial class AutoSky : Form
    {
        [ComVisible(true)]
        public class ScriptManager
        {
            // Links up the Form with the Javascript ScriptManager
            private readonly AutoSky _autoSkyForm;
            public ScriptManager(AutoSky autoSkyForm)
            {
                this._autoSkyForm = autoSkyForm;
            }

            /*
             * The below methods are used to interact client-side with server-side 
             */

            public void GoogleSkyStatus(string status)
            {
                _autoSkyForm.GoogleSkyStatus(status);
            }
        }
        private Placemarks _placemarks;
        private Placemarks _savedPlacemarks;
        private const int DefaultRange = 12000000;
        private Regex regexRA;
        private Regex regexDEC;
        private bool isRAValid;
        private bool isDECValid;
        private bool isApiLoaded;
        private bool showDebug;

        private bool ShowDebug
        {
            get { return showDebug; }
            set
            {
                showDebug = value;
                listBoxEventMessages.Visible = showDebug;
            }
        }

        static HardwareHandler _hardwareHandler;

        public delegate void MessageHandler(CustomEventArgs a);

        public static bool IsMoveSucces;
        public static GoogleSkyCs GoogleSkyCoordinate;
        private Task task;

        public static int EventCounter = 0;

        public enum NESW { North, East, South, West };

        public enum UDLR
        {
            Stop = 0,
            StopLr,
            StopUd,
            Up,
            Down,
            Left,
            LeftSlow,
            Right,
            RightSlow
        };

        public AutoSky()
        {
            InitializeComponent();
        }



        private void AutoSky_Load(object sender, EventArgs e)
        {
            var url = Environment.CurrentDirectory;
            url = url.Substring(0, url.IndexOf("\\bin\\Debug")) + "\\" + "AutoSky.html";
            GoogleSkyWebBrowser.Url = new Uri(url);
            _savedPlacemarks = XmlHelper.ReadPlacemarkXml();
            _placemarks = new Placemarks();
            _placemarks.Items = new List<Placemark>();
            _placemarks.Items.AddRange(_savedPlacemarks.Items);
            _placemarks.Items.AddRange(ExcelHelper.readPlanets().Items);
            regexRA = new Regex(@"^(?:([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9])(.[0-9][0-9]?)?$");
            regexDEC = new Regex(@"[0-8][0-9]:[0-5][0-9]:[0-5][0-9](.[0-9][0-9]?)?$");
            isRAValid = false;
            isDECValid = false;


            _hardwareHandler = new HardwareHandler();
            _hardwareHandler.ArduinoMessageEvent += Listener;
            GoogleSkyWebBrowser.ObjectForScripting = new ScriptManager(this);

            task = new Task(() => _hardwareHandler.ConnectToArduino());
            task.Start();


        }



        /// <summary>
        /// Retrieves the google sky status from the javascript object to see if it got initialized
        /// </summary>
        /// <param name="status"></param>
        private void GoogleSkyStatus(string status)
        {
            bool isConnected;
            if (Boolean.TryParse(status, out isConnected))
            {
                if (isConnected)
                {
                    UpdateGoogleSkyUi("Google Sky Connected");

                    isApiLoaded = true;

                    foreach (Placemark placemark in _placemarks.Items)
                    {
                        comboBoxPOI.Items.Add(placemark);
                        if (!String.IsNullOrEmpty(placemark.ImageLink))
                        {
                            GoogleSkyWebBrowser.Document.InvokeScript("addPlacemarkWithIcon", new object[] { 
                                new object[] { placemark.Latitude, placemark.Longitude, placemark.ImageLink }});
                        }
                    }
                }
                else
                {
                    UpdateGoogleSkyErrorUi("Google Sky Not Connected");
                }
            }
            else
            {
                UpdateGoogleSkyErrorUi(String.Format("ERROR: {0}", status));
            }
        }

        /// <summary>
        /// Converts an array passed from javascript to a string array that can be read
        /// </summary>
        /// <param name="jsArray"></param>
        /// <returns></returns>
        private static string[] ConvertJsToArray(object jsArray)
        {
            var arrayLength = (int)jsArray.GetType().InvokeMember("length", BindingFlags.GetProperty, null, jsArray, new object[] { });

            var array = new string[arrayLength];

            for (var index = 0; index < arrayLength; index++)
            {
                array[index] = jsArray.GetType().InvokeMember(index.ToString(), BindingFlags.GetProperty, null, jsArray, new object[] { }).ToString();
            }

            return array;
        }

        private void btnMapToTelescope_Click(object sender, EventArgs e)
        {
            var returnVal = GoogleSkyWebBrowser.Document.InvokeScript("sendGoogleSkyCoordinates");
            var coordinates = ConvertJsToArray(returnVal);
            LogListBox("Sending telescope coordinates \n Latitiude:{0} \n Longitude:{1}", coordinates[0], coordinates[1]);
            new TaskFactory().StartNew(() => _hardwareHandler.RequestNewPosition(new GoogleSkyCs(Double.Parse(coordinates[0]), Double.Parse(coordinates[1]))));
        }
        private void btnTelescopeToMap_Click(object sender, EventArgs e)
        {
            GoogleSkyCoordinate = new GoogleSkyCs(Double.MaxValue, Double.MaxValue);
            _hardwareHandler.GetOrientation();
        }

        #region Test Coordinates
        private void btnNorth_Click(object sender, EventArgs e)
        {
            var coordinates = HorizontalToGoogleSkyDouble(0, 45);

        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            var coordinates = HorizontalToGoogleSkyDouble(90, 45);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {

            var coordinates = HorizontalToGoogleSkyDouble(180, 45);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            var coordinates = HorizontalToGoogleSkyDouble(270, 45);
        }
        #endregion

        /// <summary>
        /// Converts horizontal coordinates to coordinates the Google Sky object can process
        /// </summary>
        /// <param name="azimuth"></param>
        /// <param name="altitude"></param>
        /// <returns> Returns the latitude, longitude, and the zoom to the Google Sky object</returns>
        private double[] HorizontalToGoogleSkyDouble(int azimuth, int altitude)
        {
            var horizontal = new HorizontalCs(azimuth, altitude);
            try
            {
                GoogleSkyCoordinate = CoordinateConverter.HorizontalToGoogleSky(horizontal);
                return new[] { GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude, 9000 };
            }
            catch (Exception ex)
            {
                LogListBox("Exception occured: {0}", ex.Message);
                return null;
            }
        }

        #region Handle Events
        /// <summary>
        /// Handles the events that are invoked from the hardware handler
        /// </summary>
        private void Listener(CustomEventArgs a)
        {
            EventCounter++;
            if (a is GpsEvent)
                new Task(() => HandleGpsEvent(a as GpsEvent)).Start();
            else if (a is ConnectionEvent)
                new Task(() => HandleConnectionEvent(a as ConnectionEvent)).Start();
            else if (a is OrientationEvent)
                new Task(() => HandleOrientationEvent(a as OrientationEvent)).Start();
            else if (a is MovementEvent)
                new Task(() => HandleMovementEvent(a as MovementEvent)).Start();
            else if (a is ErrorEvent)
                new Task(() => LogListBox(((ErrorEvent)a).Message)).Start();
            else
                throw new NotImplementedException();
        }
        private void HandleMovementEvent(MovementEvent movementEvent)
        {
            if (movementEvent == null) return;
            IsMoveSucces = true;
            LogListBox("Telescope moved successfully");
        }
        private void HandleOrientationEvent(OrientationEvent orientationEvent)
        {
            if (orientationEvent == null) return;
            if (GoogleSkyWebBrowser.InvokeRequired)
            {
                GoogleSkyWebBrowser.Invoke(new Action(() => HandleOrientationEvent(orientationEvent)));
                return;
            }
            GoogleSkyCoordinate = orientationEvent.Orientation;
            LogListBox("Update Google Sky coordinates \n Latitiude:{0} \n Longitude:{1}", GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude);
            GoogleSkyWebBrowser.Document.InvokeScript("updateGoogleSkyCoordinates",
                new object[] { new object[] { GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude, DefaultRange } });
        }
        private void HandleConnectionEvent(ConnectionEvent connectionEvent)
        {
            if (connectionEvent == null) return;
            var result = connectionEvent.Connected;
            if (result)
            {
                UpdateTelescopeUi("Connection Established");
                GoogleSkyCoordinate = new GoogleSkyCs(Double.MaxValue, Double.MaxValue);
                LogListBox("Retrieving GPS coordinates from current location...");
                _hardwareHandler.GetGpsCoOrdinates();
            }

            if (result) return;

            UpdateTelescopeErrorUi("Connection Failed");
            _hardwareHandler.ConnectToArduino();
            CurrentTask("Attempting to reconnect");
        }
        private void HandleGpsEvent(GpsEvent gpsEvent)
        {
            if (gpsEvent == null) return;
            if (GoogleSkyWebBrowser.InvokeRequired)
            {
                GoogleSkyWebBrowser.Invoke(new Action(() => HandleGpsEvent(gpsEvent)));
                return;
            }
            GoogleSkyCoordinate = new GoogleSkyCs(gpsEvent.Latitude, gpsEvent.Longitude);

            LogListBox("Got GPS coordinates \n Latitiude:{0} \n Longitude:{1}", GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude);

            var scriptArguements = new object[] { GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude, DefaultRange };
            try
            {
                if (GoogleSkyWebBrowser.Document != null)
                    GoogleSkyWebBrowser.Document.InvokeScript("updateGoogleSkyCoordinates", scriptArguements);
            }
            catch
            {
                UpdateGoogleSkyErrorUi("Unable to update sky coordinates");
            }
        }
        #endregion

        private void CurrentTask(string message)
        {
            LogListBox(message);
        }

        private void UpdateTelescopeErrorUi(string message)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => UpdateTelescopeErrorUi(message)));
                return;
            }
            telescopestatusLabel.ForeColor = Color.Red;
            telescopestatusLabel.Text = message;
            LogListBox(message);
        }

        private void UpdateTelescopeUi(string message)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => UpdateTelescopeUi(message)));
                return;
            }
            telescopestatusLabel.ForeColor = Color.Green;
            telescopestatusLabel.Text = message;
            LogListBox(message);
        }

        private void UpdateGoogleSkyErrorUi(string message)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => UpdateGoogleSkyErrorUi(message)));
                return;
            }
            googleskystatuslabel.ForeColor = Color.Red;
            googleskystatuslabel.Text = message;
            LogListBox(message);
        }

        private void UpdateGoogleSkyUi(string message)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => UpdateGoogleSkyUi(message)));
                return;
            }
            googleskystatuslabel.ForeColor = Color.Green;
            googleskystatuslabel.Text = message;
            LogListBox(message);
        }

        private void LogListBox(string message, params object[] args)
        {
            if (listBoxEventMessages.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => LogListBox(message, args)));
                return;
            }
            if (message.Contains("{0}"))
            { message = String.Format(message, args); }
            taskprogress.Text = message.Replace("\n", " "); ;
            try
            {
                listBoxEventMessages.Items.AddRange(message.Split('\n'));
            }
            catch
            {
                listBoxEventMessages.Items.Add(string.Format("Malformed message: \"{0}\"", message));
            }
        }

        private void btnMoveMap_Click(object sender, EventArgs e)
        {
            if (!isDECValid || !isRAValid)
            {
                MessageBox.Show("Invalid coordinates, please check your right ascencion and declination coordinates", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var RA = Array.ConvertAll(txtBoxCoordinateRA.Text.Split(':'), double.Parse);
            var DEC = Array.ConvertAll(txtBoxCoordinateDEC.Text.Split(':'), double.Parse);
            GoogleSkyCoordinate = CoordinateConverter.EquatorialToGoogleSky(new EquatorialCs(RA, DEC));

            LogListBox("Update Google Sky coordinates \n Latitiude:{0} \n Longitude:{1}", GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude);

            GoogleSkyWebBrowser.Document.InvokeScript("updateGoogleSkyCoordinates",
                new object[] { new object[] { GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude, DefaultRange } });
        }

        private void btnSelectPOI_Click(object sender, EventArgs e)
        {
            var selectedPlacemark = (Placemark)comboBoxPOI.SelectedItem;
            if (comboBoxPOI.SelectedItem == null) return;
            var range = selectedPlacemark.Range;
            GoogleSkyCoordinate = new GoogleSkyCs(selectedPlacemark.Latitude, selectedPlacemark.Longitude);

            GoogleSkyWebBrowser.Document.InvokeScript("addPlacemark", new object[] { 
                                new object[] { selectedPlacemark.Latitude, selectedPlacemark.Longitude, selectedPlacemark.Name }});

            LogListBox("Update Google Sky coordinates \n Latitiude:{0} \n Longitude:{1}", GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude);
            if (GoogleSkyWebBrowser.Document != null)
                GoogleSkyWebBrowser.Document.InvokeScript("updateGoogleSkyCoordinates",
                    new object[] { new object[] { GoogleSkyCoordinate.Latitude, GoogleSkyCoordinate.Longitude, range } });
        }

        private void btnSavePOI_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxSavePOI.Text))
            {
                var returnVal = GoogleSkyWebBrowser.Document.InvokeScript("sendGoogleSkyCoordinates");
                var coordinates = ConvertJsToArray(returnVal);
                var newPlacemark = new Placemark
                {
                    Name = txtBoxSavePOI.Text,
                    Latitude = double.Parse(coordinates[0]),
                    Longitude = double.Parse(coordinates[1]),
                    Range = double.Parse(coordinates[2])
                };
                _savedPlacemarks.Items.Add(newPlacemark);
                if (XmlHelper.WritePlacemarkXml(_savedPlacemarks) == 0)
                {
                    comboBoxPOI.Items.Add(newPlacemark);
                    LogListBox("Added new point of interest (Name:{0}, Latitude:{1}, Longitude:{2}, Range:{3}",
                        newPlacemark.Name, newPlacemark.Latitude, newPlacemark.Longitude, newPlacemark.Range);
                }
                else
                {
                    LogListBox("Error occured when writing to the xml file, point of interest not saved.");
                }
            }
        }

        private void AutoSky_FormClosing(object sender, FormClosingEventArgs e)
        {
            _hardwareHandler.CloseConnection();
        }

        private void btnTele_MouseUp(object sender, MouseEventArgs e)
        {
            Thread.Sleep(10);
            _hardwareHandler.StartMovement(UDLR.Stop);
        }

        private void btnTeleUp_MouseDown(object sender, MouseEventArgs e)
        {
            _hardwareHandler.StartMovement(UDLR.Up);
        }

        private void btnTeleRight_MouseDown(object sender, MouseEventArgs e)
        {
            _hardwareHandler.StartMovement(UDLR.Right);
        }

        private void btnTeleDown_MouseDown(object sender, MouseEventArgs e)
        {
            _hardwareHandler.StartMovement(UDLR.Down);
        }

        private void btnTeleLeft_MouseDown(object sender, MouseEventArgs e)
        {
            _hardwareHandler.StartMovement(UDLR.Left);

        }

        private void txtBoxCoordinateRA_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxCoordinateRA.Text))
            {
                if (regexRA.IsMatch(txtBoxCoordinateRA.Text))
                {
                    //TODO: Add checkmark or red cross images to indicate if format is correct
                    isDECValid = true;
                    imgCheckRA.Visible = true;
                    imgErrorRA.Visible = false;
                }
                else
                {
                    isDECValid = false;
                    imgCheckRA.Visible = false;
                    imgErrorRA.Visible = true;
                }
            }
            else
            {
                isDECValid = false;
                imgCheckRA.Visible = false;
                imgErrorRA.Visible = false;
            }

        }

        private void txtBoxCoordinateDEC_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxCoordinateDEC.Text))
            {
                if (regexDEC.IsMatch(txtBoxCoordinateDEC.Text))
                {
                    isRAValid = true;
                    imgCheckDec.Visible = true;
                    imgErrorDec.Visible = false;
                }
                else
                {
                    isRAValid = false;
                    imgCheckDec.Visible = false;
                    imgErrorDec.Visible = true;
                }
            }
            else
            {
                isRAValid = false;
                imgCheckDec.Visible = false;
                imgErrorDec.Visible = false;
            }
        }

        private void GoogleSkyWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            // Try to prevent navigation to other pages.
            if (!isApiLoaded || !e.Url.AbsoluteUri.Contains("http")) return;
            Process.Start(e.Url.AbsoluteUri);
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(10);
            _hardwareHandler.stop = true;
            _hardwareHandler.StartMovement(UDLR.Stop);
        }

        private void AutoSky_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F8)
                ShowDebug = !ShowDebug;

        }

        private void AutoSky_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
