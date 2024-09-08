using System;

namespace Shared.Common.Network.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException() : base("Not connected."){}
    }
}