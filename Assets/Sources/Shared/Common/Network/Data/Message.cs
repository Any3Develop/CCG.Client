using System;

namespace Shared.Common.Network.Data
{
    public class Message
    {
        public string Target { get; set; }
        public byte[][] Args { get; set; } = Array.Empty<byte[]>();
    }
}