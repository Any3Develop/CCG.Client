using System;

namespace Shared.Common.Network
{
    public class NotAuthrorizedException : Exception
    {
        public NotAuthrorizedException(string message = null) : base(message ?? "Not authorized."){}
    }
}