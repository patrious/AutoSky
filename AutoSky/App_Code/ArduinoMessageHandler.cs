using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoSky.CoOrdinate_Systems;

namespace AutoSky
{
    public class OrientationEvent : CustomEventArgs
    {
        public GoogleSkyCs Orientation;

        public OrientationEvent(GoogleSkyCs orientation)
        {
            Orientation = orientation;

        }
    }

    public class ConnectionEvent : CustomEventArgs
    {
        public bool Connected;
        public ConnectionEvent(bool connected)
        {
            Connected = connected;
        }
    }

    public class MovementEvent : CustomEventArgs
    {
        public bool CompletedMove;
        public MovementEvent(bool completedMove)
        {
            CompletedMove = completedMove;
        }
    }

    public class ErrorEvent : CustomEventArgs
    {
        public string Message;

        public ErrorEvent(string message)
        {
            Message = message;
        }
    }


    public class CustomEventArgs
    {

    }
}