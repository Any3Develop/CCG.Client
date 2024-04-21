using System;

namespace Core.Network.ArtRepository
{
    public class RemoteArtException : Exception
    {
        public string ArtId { get; }

        public RemoteArtException(string message, string artId) : base(message)
        {
            ArtId = artId;
        }
    }
}