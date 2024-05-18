using System;

namespace Client.Common.Network.Exceptions
{
    public class NotAuthrorizedException : Exception
    {
        public NotAuthrorizedException(string message) : base(message ?? "Not authorized."){}
    }
}