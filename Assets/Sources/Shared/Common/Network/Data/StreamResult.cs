namespace Shared.Common.Network.Data
{
    public readonly struct StreamResult
    {
        public bool Successful { get; }
        public byte[] Buffer { get; }

        public StreamResult(byte[] buffer)
        {
            Successful = buffer is {Length: > 0};
            Buffer = buffer;
        }
    }
}