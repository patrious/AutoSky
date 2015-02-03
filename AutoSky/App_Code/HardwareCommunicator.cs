#define DEBUG

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSky
{
    /// <summary>
    /// Summary description for HardwareCommunicator
    /// </summary>
    public class HardwareCommunicator : IDisposable
    {

        public bool Shutdown = false;


        SerialPort connection = new SerialPort();
        Thread incomingThread;
        Thread outgoingThread;
        Thread passdataThread;

        HardwareHandler hwh;
        // Spin up a thread to check the queue and send commands down to the hardare.
        Queue<String> incoming_queue = new Queue<String>();

        //Spin up a thread to wait on messages comming up from the hardware. Pass them up to the Hardware Handler as appropiate.
        Queue<String> outgoing_queue = new Queue<String>();


        //I want to make events to callback to the hardware handler.
        public HardwareCommunicator(HardwareHandler hardwareHandler)
        {
            try
            {
                outgoingThread.Abort();
            }
            catch { }
            try
            {
                incomingThread.Abort();
            }
            catch { }
            try
            {
                passdataThread.Abort();
            }
            catch { }
            hwh = hardwareHandler;
        }


        public void ConnectToArduino()
        {
            try
            {
                // Connect to Arduino then create the threads.
                connection = FindArduino();

                // Alert the UI some error happened.
                if (connection == null)
                {
                    return;
                }
                try
                {
                    if (!connection.IsOpen) connection.Open();
                }
                catch
                {
                    //Issues. Alert the UI. Cannot connect to the arduino.
                    return;
                }
                hwh.ConnectedToSky = true;
                outgoingThread = new Thread(Return_Thread);
                outgoingThread.Start();

                passdataThread = new Thread(PassData_Thread);
                passdataThread.Start();

                incomingThread = new Thread(SendMessage_Thread);
                incomingThread.Start();

                hwh.FireEvent(new ConnectionEvent(true));
            }
            catch
            {
                hwh.FireEvent(new ConnectionEvent(false));
                // Could not connect to Arduino. Or threads are fucked.
                // Raise a stink.
            }
        }
        private SerialPort FindArduino()
        {
            var portnames = SerialPort.GetPortNames();
            // var portnames = new[] { "COM4" };
            //Ask the user if they know what port the Ardunio is on...
            //Display list box with the port names.
            //TODO: Figure out how to do this with albert
            //If failed, ask if they want to scan or quit.
            //Search for it.
            var taskFactory = new TaskFactory();
            var tokenSource = new CancellationTokenSource();
            var cancelToken = tokenSource.Token;
            var taskList = new List<Tuple<Task<bool>, SerialPort>>();
            foreach (var portname in portnames)
            {
                var sp = new SerialPort(portname);
                var task = taskFactory.StartNew(() => DetectArduino(sp), cancelToken);
                taskList.Add(new Tuple<Task<bool>, SerialPort>(task, sp));
            }
            taskList.All(x => x.Item1.Wait(new TimeSpan(0, 0, 25)));
            var winner = taskList.First(x => x.Item1.Result);
            tokenSource.Cancel();
            if (winner != null)
            {
                return winner.Item2;
            }
        
            hwh.FireEvent(new ErrorEvent("Could not connect to Telescope, Serial Port could not be found"));
            return null;
        }

        protected bool DetectArduino(SerialPort sp)
        {
            var found = false;
            try
            {
                const string msg = "AAPR;";
                sp.WriteTimeout = 500;
                sp.ReadTimeout = 500;

                sp.Open();
                Thread.Sleep(1000);
                sp.Write(msg);
                Thread.Sleep(4000);
                sp.ReadTimeout = 500;
                var returnText = "";
                if (sp.BytesToRead > 0)
                {
                    returnText = sp.ReadLine().Trim();
                }
                if (returnText.Contains("RPAA"))
                {
                    found = true;
                }
                else
                {
                    sp.Close();
                }

            }
            catch
            {
                found = false;
                sp.Close();
            }

            return found;
        }

        public int AddToQueue(String command)
        {
            try
            {
                Monitor.Enter(outgoing_queue);
                outgoing_queue.Enqueue(command);
            }
            catch
            { return -1; }
            finally
            {

                Monitor.PulseAll(outgoing_queue);
                Monitor.Exit(outgoing_queue);
            }
#if (DEBUG)
            if (outgoing_queue.Count() > 25)
            {
                hwh.FireEvent(new ErrorEvent("Outgoing queue has over 25 commands in it"));
            }
#endif
            return 0;
        }

        private void SendMessage_Thread()
        {
            while (!Shutdown)
            {
                Monitor.Enter(outgoing_queue);

                if (outgoing_queue.Count > 0)
                {
                    try
                    {
                        var command = outgoing_queue.Dequeue();
                        Thread.Sleep(200);
                        try
                        {
                            connection.Write(command);
                        }
                        catch (Exception e)
                        {
                            hwh.FireEvent(new ErrorEvent(String.Format("No connection established to Telescope, but we are trying to send a command.{0}", e)));
                        }

                    }
                    finally
                    {
                        Monitor.Exit(outgoing_queue);
                    }
                }
                else
                {
                    Monitor.Wait(outgoing_queue);
                }
            }

        }

        private void Return_Thread()
        {
            while (!Shutdown)
            {
                String returnData = null;
                try
                {
                    returnData = connection.ReadLine().Trim();
                }
                catch (Exception)
                {
                    //NOM NOM
                }
                if (returnData == null) continue;

                Monitor.Enter(incoming_queue);
                incoming_queue.Enqueue(returnData);
                Monitor.Pulse(incoming_queue);
                Monitor.Exit(incoming_queue);
            }
        }

        private void PassData_Thread()
        {
            while (!Shutdown)
            {
                Monitor.Enter(incoming_queue);

                if (incoming_queue.Count > 0)
                {
                    try
                    {
                        hwh.RecieveData(incoming_queue.Dequeue());
                    }
                    finally
                    {
                        Monitor.Exit(incoming_queue);
                    }
                }
                else
                {
                    Monitor.Wait(incoming_queue);
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Shutdown = true;
            if (connection != null)
                connection.Dispose();
            if (incomingThread != null)
                incomingThread.Abort();
            if (outgoingThread != null)
                outgoingThread.Abort();
            if (passdataThread != null)
                passdataThread.Abort();
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Shutdown = true;
            connection.Close();
            incomingThread.Abort();
            outgoingThread.Abort();
            passdataThread.Abort();
            Dispose();
        }

        #endregion
    }
}