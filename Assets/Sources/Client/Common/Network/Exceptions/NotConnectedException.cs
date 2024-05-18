using System;

namespace Client.Common.Network.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException() : base("Not connected."){}
    }
}