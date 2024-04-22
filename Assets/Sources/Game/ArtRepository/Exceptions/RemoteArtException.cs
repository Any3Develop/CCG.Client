using System;

namespace Core.ArtRepository
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